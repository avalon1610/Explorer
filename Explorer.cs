using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System;

namespace Explorer
{
    public partial class Explorer : Form
    {
        public Explorer()
        {
            InitializeComponent();
            tvMain.ImageIndex = (int)ICON_TYPE.FOLDER;
            tvMain.SelectedImageIndex = (int)ICON_TYPE.FOLDER;
        }

        public void addmodel(string model)
        {
            cbDevice.Items.Add(model);
            cbDevice.SelectedIndex = 0;
        }

        private enum ICON_TYPE
        {
            DRIVER = 0,
            FOLDER = 1,
            FIlE = 2
        }

        public void clearmodel()
        {
            cbDevice.Items.Clear();
        }

        public void clearlog()
        {
            rtbLog.Clear();
        }

        public void setlog(string text)
        {
            if (!string.IsNullOrEmpty(text))
                rtbLog.AppendText(text + "\n");
        }

        private void tbCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Key_Enter)
            {
                manager.OnCommand(tbCommand.Text, true);
            }
        }

        const int Key_Enter = 13;
        private Manager manager;

        private void Explorer_Shown(object sender, System.EventArgs e)
        {
            manager = new Manager(this);   
        }

        public void addtree(List<string> items)
        {
            tvMain.SelectedNode.Nodes.Clear();
            foreach (string item in items)
                tvMain.SelectedNode.Nodes.Add(item,item);
        }

        public void addnode(string key, string text)
        {
            TreeNode node = tvMain.Nodes.Add(key, text);
            node.ImageIndex = (int)ICON_TYPE.DRIVER;
            node.SelectedImageIndex = (int)ICON_TYPE.DRIVER;
        }

        public void cleartree()
        {
            tvMain.Nodes.Clear();
        }

        public void clearfiles()
        {
            lvMain.Items.Clear();
        }

        public void addfiles(List<string> files)
        {
            ListViewItem item;
            foreach (string file in files)
            {
                item = lvMain.Items.Add(file);
                item.ImageIndex = (int)ICON_TYPE.FIlE;
            }
        }

        public void adddirs(List<string> dirs)
        {
            ListViewItem item;
            foreach (string dir in dirs)
            {
                item = lvMain.Items.Add(dir);
                item.ImageIndex = (int)ICON_TYPE.FOLDER;
            }
        }

        private void btnCall_Click(object sender, System.EventArgs e)
        {
            manager.OnCommand(tbCommand.Text, true);
        }

        private void btnFlush_Click(object sender, System.EventArgs e)
        {
            manager.FlushDevices();
        }

        public string GetCurrentDevice()
        {
            return cbDevice.SelectedItem.ToString();
        }

        private string GetPath(TreeNode node)
        {
            string path = "";
            do
            {
                path = node.Name + "/" + path;
                node = node.Parent;
            } while (node != null);
            path = "/" + path;
            return path;
        }

        private void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvMain.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
                return;
            string path = GetPath(e.Node);
            if (!tvMain.SelectedNode.IsExpanded)
            {
                manager.Do("FlushFiles", path);
            }
            manager.Do("UpdateTree", path);
            manager.Do("UpdateView", path);
        }

        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (object obj in (e.Data.GetData(DataFormats.FileDrop) as System.Array))
                {
                    manager.Do("Push", obj.ToString());
                }
                string device_path = GetPath(tvMain.SelectedNode);
                manager.Do("FlushFiles", device_path);
                manager.Do("UpdateTree", device_path);
                manager.Do("UpdateView", device_path);
            }
        }

        private void lvMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (tvMain.SelectedNode == null)
                return;
            if (lvMain.GetItemAt(e.X, e.Y) == null)
                lvMain.ContextMenuStrip = cmsBlank;
            else
            {
                if (lvMain.SelectedItems.Count > 1)
                    cmsItem.Items.Find("ActionRename", false)[0].Enabled = false;
                lvMain.ContextMenuStrip = cmsItem;
            }
        }

        private void tvMain_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = tvMain.GetNodeAt(e.X,e.Y);
            if (node == null || node.Parent == null)
                tvMain.ContextMenuStrip = null;
            else
                tvMain.ContextMenuStrip = cmsItem;
        }

        private void ActionDelete_Click(object sender, System.EventArgs e)
        {
            string path;
            if (lvMain.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in lvMain.SelectedItems)
                {
                    manager.Do("Delete", item.Text);
                }
                path = GetPath(tvMain.SelectedNode);
            }
            else
            {
                TreeNode tempnode = tvMain.SelectedNode;
                tvMain.SelectedNode = tvMain.SelectedNode.Parent;
                manager.Do("Delete", tempnode.Text);
                path = GetPath(tvMain.SelectedNode);
            }
            manager.Do("FlushFiles", path);
            manager.Do("UpdateTree", path);
            manager.Do("UpdateView", path);
        }

        Func<string, string> addQuote = new Func<string, string>(a => { return "\"" + a + "\""; });

        private void lvMain_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<string> selection = new List<string>();
            string path = GetPath(tvMain.SelectedNode);
            foreach (ListViewItem item in lvMain.SelectedItems)
            {
                manager.Do("Pull", addQuote(path + item.Text));
                selection.Add(Path.GetTempPath() + "/" + item.Text);
            }
            DataObject data = new DataObject(DataFormats.FileDrop, selection.ToArray());
            DoDragDrop(data, DragDropEffects.Copy);
        }

        private void lvMain_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void lvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvMain.GetItemAt(e.X,e.Y);
            if (item == null || item.ImageIndex != (int)ICON_TYPE.FOLDER)
                return;
            string path = GetPath(tvMain.SelectedNode) + item.Text;
            tvMain.SelectedNode = tvMain.SelectedNode.Nodes.Find(item.Text,false)[0];
            manager.Do("FlushFiles", path);
            manager.Do("UpdateTree", path);
            manager.Do("UpdateView", path);
        }

        private void cbDevice_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            manager.Do("ShowRoot");
        }

        private void ActionNewFolder_Click(object sender, System.EventArgs e)
        {
            ListViewItem item = lvMain.Items.Add("NewFolder");
            item.ImageIndex = (int)ICON_TYPE.FOLDER;
            isNewFolder = true;
            item.BeginEdit();
        }

        private bool isNewFolder = false;
        private string beforename = "";
        private void lvMain_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string label = e.Label == null ? "NewFolder" : e.Label;
            if (isNewFolder)
                manager.Do("Mkdir", label);
            else
                manager.Do("Rename",beforename,label);
            string path = GetPath(tvMain.SelectedNode);
            manager.Do("FlushFiles", path);
            manager.Do("UpdateTree", path);
            isNewFolder = false;
        }

        private void tvMain_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                return;
            manager.Do("Rename", beforename, e.Label);
            string path = GetPath(tvMain.SelectedNode.Parent);
            tvMain.SelectedNode = tvMain.SelectedNode.Parent;
            manager.Do("FlushFiles", path);
            manager.Do("UpdateView", path);
        }

        private void ActionRename_Click(object sender, System.EventArgs e)
        {
            if (lvMain.SelectedItems.Count > 0)
            {
                var item = lvMain.SelectedItems[0];
                beforename = item.Text;
                item.BeginEdit();
            }
            else
            {
                beforename = tvMain.SelectedNode.Text;
                tvMain.SelectedNode.BeginEdit();
            }
        }

        private void cmsItem_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            cmsItem.Items.Find("ActionRename", false)[0].Enabled = true;
        }
    }
}
