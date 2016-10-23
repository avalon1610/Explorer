using System;
using System.Diagnostics;
using System.Text;

namespace Explorer
{
    public delegate void CallCMDCompletedEventHandler(CallCMDCompletedEventArgs e);
    public class CallCMDCompletedEventArgs
    {
        private string result = "";
        private Exception ex = null;
        public string Result { get { return result; } }
        public Exception Ex { get { return ex; } }
        public CallCMDCompletedEventArgs(string result, Exception ex)
        {
            this.result = result;
            this.ex = ex;
        }
    }

    class Command
    {
        private Explorer explorer;
        public Command(Explorer explorer)
        {
            this.explorer = explorer;
        }

        private void InitializeSyncCmd()
        {
            cmd = new Process();
            cmd.StartInfo.FileName = "adb.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            cmd.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            cmd.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
        }

        private void InitializeAsyncCmd()
        {
            cmd_async = new Process();
            cmd_async.StartInfo.FileName = "adb.exe";
            cmd_async.StartInfo.UseShellExecute = false;
            cmd_async.StartInfo.RedirectStandardOutput = true;
            cmd_async.StartInfo.RedirectStandardError = true;
            cmd_async.StartInfo.CreateNoWindow = true;
            cmd_async.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd_async.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            cmd_async.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            cmd_async.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
            cmd_async.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
        }

        private Process cmd_async = null;
        private Process cmd = null;

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null || string.IsNullOrEmpty(e.Data))
                return;
            CallCMDCompleted(new CallCMDCompletedEventArgs(e.Data, null));
        }

        private void ErrorHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null || string.IsNullOrEmpty(e.Data))
                return;
            CallCMDCompleted(new CallCMDCompletedEventArgs(e.Data, null));
        }

        public void CancalAsync()
        {
            cmd_async.Kill();
            cmd_async.Close();
        }

        public event CallCMDCompletedEventHandler CallCMDCompleted;
        public void CallCMDAsync(string command)
        {
            InitializeAsyncCmd();
            try
            {
                explorer.clearlog();
                if (command == "shell")
                {
                    explorer.setlog("交互命令模式已被屏蔽，用shell <command>代替");
                    return;
                }
                cmd_async.StartInfo.Arguments = command;
                cmd_async.Start();
                cmd_async.BeginErrorReadLine();
                cmd_async.BeginOutputReadLine();
            }
            catch (System.Exception ex)
            {
                CallCMDCompleted(new CallCMDCompletedEventArgs("", ex));
            }
        }
 
        public string CallCMD(string command, bool showlog = false)
        {
            InitializeSyncCmd();
            string output = "";
            try
            {
                explorer.clearlog();
                if (command == "shell")
                {
                    explorer.setlog("交互命令模式已被屏蔽，用shell <command>代替");
                    return "";
                }
                cmd.StartInfo.Arguments = command;
                cmd.Start();
                cmd.BeginErrorReadLine();
                output = cmd.StandardOutput.ReadToEnd();
                if (showlog)
                    explorer.setlog(output);
                cmd.WaitForExit();
                cmd.Close();
            }
            catch (System.Exception ex)
            {
                explorer.setlog(ex.Message);
            }
            return output;
        }
    }
}
