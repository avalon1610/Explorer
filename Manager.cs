using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Explorer
{
    class Manager
    {
        const string get_model_cmd = "shell cat /system/build.prop | grep \"ro.product.model\"";
        const string get_devices_cmd = "devices";
        const string get_dir_cmd = "shell ls -l ";
        const string dir_error = "No such file or directory";
        const string root_dir = "/";
        const string sdcard0_dir = "storage/sdcard0";
        const string sdcard1_dir = "storage/sdcard1";
        const string push_cmd = "push ";
        const string pull_cmd = "pull ";
        const string time_regex = @"(?:[01][0-9]|2[0-3]):[0-5][0-9]";
        const string delete_cmd = "shell rm -rf ";
        const string mkdir_cmd = "shell mkdir ";
        const string mv_cmd = "shell mv ";

        private Command command;
        private Explorer explorer;
        public Manager(Explorer explorer)
        {
            command = new Command(explorer);
            this.explorer = explorer;
            command.CallCMDCompleted += new CallCMDCompletedEventHandler(AsyncOperationCompleted);
            FlushDevices();
            if (devices.Count > 0)
            {
                ShowRoot(devices[0]);
            }
        }

        public void OnCommand(string cmd, bool showlog)
        {
            command.CallCMD(cmd, showlog);
        }

        private void ShowRoot(Device device)
        {
            explorer.cleartree();
            if (TestDir(device, sdcard0_dir))
                explorer.addnode(Manager.sdcard0_dir, "内部存储");
            if (TestDir(device, sdcard1_dir))
                explorer.addnode(Manager.sdcard1_dir, "SD卡");
        }

        private bool TestDir(Device device, string path)
        {
            string output = command.CallCMD(device.prefix + get_dir_cmd + path);
            if (output.IndexOf(dir_error) != -1)
                return false;
            return true;
        }

        public void FlushDevices()
        {
            explorer.Invoke(new Action(() => { explorer.clearmodel(); }));
            devices = new List<Device>();
            string[] devices_array = Regex.Split(command.CallCMD(get_devices_cmd), "\r\n", RegexOptions.IgnoreCase);
            foreach (string device in devices_array)
            {
                if (device.IndexOf('\t') == -1)
                    continue;
                var dev = new Device(device.Split('\t')[0]);
                var model = command.CallCMD(dev.prefix + get_model_cmd);
                if (string.IsNullOrEmpty(model))
                    continue;
                dev.model = model.Split('=')[1].TrimEnd("\r\n".ToCharArray());
                explorer.Invoke(new Action(() => { explorer.addmodel(dev.model); }));
                devices.Add(dev);
            }
        }

        public void Do(string methodname, params object[] argv)
        {
            string model = explorer.GetCurrentDevice();
            foreach (Device d in devices)
            {
                if (d.model == model)
                {
                    int argc = argv.GetLength(0);
                    object[] parameters = new object[argc + 1];
                    Type[] types = new Type[argc + 1];
                    parameters[0] = d;
                    types[0] = d.GetType(); ;
                    for (int i = 0; i < argc; ++i)
                    {
                        types[i + 1] = argv[i].GetType();
                        parameters[i + 1] = argv[i];
                    }
                    MethodInfo method = this.GetType().GetMethod(methodname,
                        BindingFlags.Instance | BindingFlags.NonPublic, null, types, null);
                    if (method == null)
                    {
                        MessageBox.Show("调用错误");
                        return;
                    }
                    method.Invoke(this, parameters);
                    break;
                }
            }
        }

        private void FlushFiles(Device device, string path)
        {
            var dir = command.CallCMD(device.prefix + get_dir_cmd + path);
            if (dir.IndexOf(dir_error) != -1)
                return;
            device.currentpath = path;
            string[] items = Regex.Split(dir, "\r\r\n");
            List<string> dirs = new List<string>();
            List<string> files = new List<string>();
            foreach (string item in items)
            {
                Match m = Regex.Match(item, time_regex);
                if (!m.Success)
                    continue;
                if (!item.StartsWith("d"))
                {
                    files.Add(item.Substring(m.Index + 6));
                    continue;
                }
                dirs.Add(item.Substring(m.Index + 6));
            }
            device.filecollection[path] = new Tuple<List<string>, List<string>>(dirs, files);
        }

        private void UpdateTree(Device device, string path)
        {
            if (device.filecollection.Contains(path))
            {
                var collection = device.filecollection[path] as Tuple<List<string>, List<string>>;
                List<string> dirs = collection.Item1;
                explorer.Invoke(new Action(() => { explorer.addtree(dirs); }));
            }
        }

        private void UpdateView(Device device, string path)
        {
            if (device.filecollection.Contains(path))
            {
                explorer.Invoke(new Action(() => { explorer.clearfiles(); }));
                var collection = device.filecollection[path] as Tuple<List<string>, List<string>>;
                List<string> dirs = collection.Item1;
                explorer.Invoke(new Action(() => { explorer.adddirs(dirs); }));
                List<string> files = collection.Item2;
                explorer.Invoke(new Action(() => { explorer.addfiles(files); }));
            }
        }

        public void AsyncOperationCompleted(CallCMDCompletedEventArgs e)
        {
            explorer.BeginInvoke(new Action(() =>
            {
                if (string.IsNullOrEmpty(e.Result))
                    explorer.setlog(e.Ex.Message);
                else
                    explorer.setlog(e.Result);
            }));
        }

        private void Push(Device device, string path)
        {
            command.CallCMDAsync(device.prefix + push_cmd + path + " " + device.currentpath);
        }

        private void Delete(Device device, string name)
        {
            command.CallCMD(device.prefix + delete_cmd + device.currentpath + "/" + name);
        }

        private void Pull(Device device, string path)
        {
            string item = path.Substring(path.LastIndexOf("/") + 1);
            string temp = Path.GetTempPath() + item;
            command.CallCMDAsync(device.prefix + pull_cmd + path + " \"" + temp);
        }

        private void Mkdir(Device device, string path)
        {
            command.CallCMD(device.prefix + mkdir_cmd + device.currentpath + path);
        }

        private void Rename(Device device, string oldname, string newname)
        {
            command.CallCMD(device.prefix + mv_cmd + device.currentpath + oldname + " " + device.currentpath + newname);
        }

        private List<Device> devices;
        public class Device
        {
            public Device(string serial)
            {
                this.serial = serial;
                prefix = "-s " + serial + " ";
            }
            public string currentpath { get; set; }
            public string model { get; set; }
            public string prefix { get; set; }
            public HybridDictionary filecollection = new HybridDictionary();
            private string serial;
        }
    }


}
