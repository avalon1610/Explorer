namespace Explorer
{
    partial class Explorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Explorer));
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.lvMain = new System.Windows.Forms.ListView();
            this.cmsItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ActionDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tvMain = new System.Windows.Forms.TreeView();
            this.btnFlush = new System.Windows.Forms.Button();
            this.btnCall = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.cmsBlank = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ActionNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ActionRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsItem.SuspendLayout();
            this.cmsBlank.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDevice
            // 
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(12, 12);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(194, 20);
            this.cbDevice.TabIndex = 0;
            this.cbDevice.SelectionChangeCommitted += new System.EventHandler(this.cbDevice_SelectionChangeCommitted);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.Location = new System.Drawing.Point(12, 395);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(760, 155);
            this.rtbLog.TabIndex = 4;
            this.rtbLog.Text = "";
            // 
            // tbCommand
            // 
            this.tbCommand.Location = new System.Drawing.Point(294, 11);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(397, 21);
            this.tbCommand.TabIndex = 1;
            this.tbCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommand_KeyPress);
            // 
            // lvMain
            // 
            this.lvMain.AllowDrop = true;
            this.lvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMain.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.LabelEdit = true;
            this.lvMain.Location = new System.Drawing.Point(212, 40);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(560, 349);
            this.lvMain.SmallImageList = this.imageList;
            this.lvMain.TabIndex = 3;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.List;
            this.lvMain.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvMain_AfterLabelEdit);
            this.lvMain.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvMain_ItemDrag);
            this.lvMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvMain_DragDrop);
            this.lvMain.DragOver += new System.Windows.Forms.DragEventHandler(this.lvMain_DragOver);
            this.lvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDoubleClick);
            this.lvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDown);
            // 
            // cmsItem
            // 
            this.cmsItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActionDelete,
            this.toolStripSeparator1,
            this.ActionRename});
            this.cmsItem.Name = "cmsItem";
            this.cmsItem.Size = new System.Drawing.Size(113, 54);
            this.cmsItem.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.cmsItem_Closing);
            // 
            // ActionDelete
            // 
            this.ActionDelete.Name = "ActionDelete";
            this.ActionDelete.Size = new System.Drawing.Size(152, 22);
            this.ActionDelete.Text = "删除";
            this.ActionDelete.Click += new System.EventHandler(this.ActionDelete_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "driver.ico");
            this.imageList.Images.SetKeyName(1, "folder.ico");
            this.imageList.Images.SetKeyName(2, "file.ico");
            // 
            // tvMain
            // 
            this.tvMain.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvMain.HideSelection = false;
            this.tvMain.ImageIndex = 0;
            this.tvMain.ImageList = this.imageList;
            this.tvMain.LabelEdit = true;
            this.tvMain.Location = new System.Drawing.Point(12, 40);
            this.tvMain.Name = "tvMain";
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.Size = new System.Drawing.Size(194, 349);
            this.tvMain.TabIndex = 5;
            this.tvMain.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvMain_AfterLabelEdit);
            this.tvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMain_NodeMouseClick);
            this.tvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMain_MouseDown);
            // 
            // btnFlush
            // 
            this.btnFlush.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFlush.Location = new System.Drawing.Point(213, 10);
            this.btnFlush.Name = "btnFlush";
            this.btnFlush.Size = new System.Drawing.Size(75, 23);
            this.btnFlush.TabIndex = 6;
            this.btnFlush.Text = "刷新设备";
            this.btnFlush.UseVisualStyleBackColor = true;
            this.btnFlush.Click += new System.EventHandler(this.btnFlush_Click);
            // 
            // btnCall
            // 
            this.btnCall.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCall.Location = new System.Drawing.Point(697, 10);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(75, 23);
            this.btnCall.TabIndex = 7;
            this.btnCall.Text = "执行命令";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(194, 349);
            this.treeView1.TabIndex = 5;
            // 
            // cmsBlank
            // 
            this.cmsBlank.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActionNewFolder});
            this.cmsBlank.Name = "cmsBlank";
            this.cmsBlank.Size = new System.Drawing.Size(137, 26);
            // 
            // ActionNewFolder
            // 
            this.ActionNewFolder.Name = "ActionNewFolder";
            this.ActionNewFolder.Size = new System.Drawing.Size(136, 22);
            this.ActionNewFolder.Text = "新建文件夹";
            this.ActionNewFolder.Click += new System.EventHandler(this.ActionNewFolder_Click);
            // 
            // ActionRename
            // 
            this.ActionRename.Name = "ActionRename";
            this.ActionRename.Size = new System.Drawing.Size(152, 22);
            this.ActionRename.Text = "重命名";
            this.ActionRename.Click += new System.EventHandler(this.ActionRename_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.btnFlush);
            this.Controls.Add(this.tvMain);
            this.Controls.Add(this.lvMain);
            this.Controls.Add(this.tbCommand);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.cbDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Explorer";
            this.Text = "Explorer";
            this.Shown += new System.EventHandler(this.Explorer_Shown);
            this.cmsItem.ResumeLayout(false);
            this.cmsBlank.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.Button btnFlush;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip cmsItem;
        private System.Windows.Forms.ToolStripMenuItem ActionDelete;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip cmsBlank;
        private System.Windows.Forms.ToolStripMenuItem ActionNewFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ActionRename;
    }
}

