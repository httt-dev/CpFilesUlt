namespace CpFiles
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.clbFiles = new System.Windows.Forms.CheckedListBox();
            this.srcFolderPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSrcFolder = new System.Windows.Forms.Button();
            this.labSrcPath = new System.Windows.Forms.Label();
            this.txtDesFolder = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.statusCpInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.ctmFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuClearContent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyContentToClipBoard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenFolderLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.btnTestAppDriver = new System.Windows.Forms.Button();
            this.txtTestAppDriver = new System.Windows.Forms.TextBox();
            this.btnScreenshotFull = new System.Windows.Forms.Button();
            this.btnScreenshotPrimary = new System.Windows.Forms.Button();
            this.btnScreenshotWindow = new System.Windows.Forms.Button();
            this.statusInfo.SuspendLayout();
            this.ctmFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // clbFiles
            // 
            this.clbFiles.CheckOnClick = true;
            this.clbFiles.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbFiles.FormattingEnabled = true;
            this.clbFiles.Location = new System.Drawing.Point(3, 64);
            this.clbFiles.Name = "clbFiles";
            this.clbFiles.Size = new System.Drawing.Size(464, 334);
            this.clbFiles.TabIndex = 0;
            this.clbFiles.SelectedIndexChanged += new System.EventHandler(this.clbFiles_SelectedIndexChanged);
            this.clbFiles.DoubleClick += new System.EventHandler(this.clbFiles_DoubleClick);
            // 
            // btnSrcFolder
            // 
            this.btnSrcFolder.Location = new System.Drawing.Point(3, 3);
            this.btnSrcFolder.Name = "btnSrcFolder";
            this.btnSrcFolder.Size = new System.Drawing.Size(224, 26);
            this.btnSrcFolder.TabIndex = 1;
            this.btnSrcFolder.Text = "open WPSaturnEMoney Folder";
            this.btnSrcFolder.UseVisualStyleBackColor = true;
            this.btnSrcFolder.Click += new System.EventHandler(this.btnSrcFolder_Click);
            // 
            // labSrcPath
            // 
            this.labSrcPath.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSrcPath.Location = new System.Drawing.Point(3, 32);
            this.labSrcPath.Name = "labSrcPath";
            this.labSrcPath.Size = new System.Drawing.Size(834, 24);
            this.labSrcPath.TabIndex = 2;
            // 
            // txtDesFolder
            // 
            this.txtDesFolder.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesFolder.Location = new System.Drawing.Point(123, 423);
            this.txtDesFolder.Name = "txtDesFolder";
            this.txtDesFolder.Size = new System.Drawing.Size(714, 26);
            this.txtDesFolder.TabIndex = 4;
            this.txtDesFolder.Text = "D:\\workspace\\Hei\\UT\\HEI_SELF-82_001";
            this.txtDesFolder.TextChanged += new System.EventHandler(this.txtDesFolder_TextChanged);
            this.txtDesFolder.DoubleClick += new System.EventHandler(this.txtDesFolder_DoubleClick);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(3, 419);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(114, 31);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Copy Selected Files";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // statusInfo
            // 
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusCpInfo});
            this.statusInfo.Location = new System.Drawing.Point(0, 458);
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(915, 22);
            this.statusInfo.TabIndex = 6;
            this.statusInfo.Text = "statusStrip1";
            // 
            // statusCpInfo
            // 
            this.statusCpInfo.Name = "statusCpInfo";
            this.statusCpInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // ctmFiles
            // 
            this.ctmFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuClearContent,
            this.menuCopyContentToClipBoard,
            this.menuOpenFolderLocation});
            this.ctmFiles.Name = "ctmFiles";
            this.ctmFiles.Size = new System.Drawing.Size(214, 70);
            // 
            // menuClearContent
            // 
            this.menuClearContent.Image = global::CpFiles.Properties.Resources._38988_edit_clear_sweep_sweeper_icon;
            this.menuClearContent.Name = "menuClearContent";
            this.menuClearContent.Size = new System.Drawing.Size(213, 22);
            this.menuClearContent.Text = "Clear content";
            this.menuClearContent.Click += new System.EventHandler(this.menuClearContent_Click);
            // 
            // menuCopyContentToClipBoard
            // 
            this.menuCopyContentToClipBoard.Image = global::CpFiles.Properties.Resources._7038097_marketing_file_business_clipboard_data_icon;
            this.menuCopyContentToClipBoard.Name = "menuCopyContentToClipBoard";
            this.menuCopyContentToClipBoard.Size = new System.Drawing.Size(213, 22);
            this.menuCopyContentToClipBoard.Text = "Copy content to clipboard";
            this.menuCopyContentToClipBoard.Click += new System.EventHandler(this.menuCopyContentToClipBoard_Click);
            // 
            // menuOpenFolderLocation
            // 
            this.menuOpenFolderLocation.Image = global::CpFiles.Properties.Resources._2993665_brand_brands_explorer_logo_logos_icon;
            this.menuOpenFolderLocation.Name = "menuOpenFolderLocation";
            this.menuOpenFolderLocation.Size = new System.Drawing.Size(213, 22);
            this.menuOpenFolderLocation.Text = "Open file location";
            this.menuOpenFolderLocation.Click += new System.EventHandler(this.menuOpenFolderLocation_Click);
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtContent.Location = new System.Drawing.Point(473, 66);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(439, 332);
            this.txtContent.TabIndex = 7;
            this.txtContent.Text = "";
            // 
            // btnTestAppDriver
            // 
            this.btnTestAppDriver.Location = new System.Drawing.Point(405, 13);
            this.btnTestAppDriver.Name = "btnTestAppDriver";
            this.btnTestAppDriver.Size = new System.Drawing.Size(75, 23);
            this.btnTestAppDriver.TabIndex = 8;
            this.btnTestAppDriver.Text = "button1";
            this.btnTestAppDriver.UseVisualStyleBackColor = true;
            this.btnTestAppDriver.Visible = false;
            this.btnTestAppDriver.Click += new System.EventHandler(this.btnTestAppDriver_Click);
            // 
            // txtTestAppDriver
            // 
            this.txtTestAppDriver.Location = new System.Drawing.Point(524, 13);
            this.txtTestAppDriver.Name = "txtTestAppDriver";
            this.txtTestAppDriver.Size = new System.Drawing.Size(155, 20);
            this.txtTestAppDriver.TabIndex = 9;
            this.txtTestAppDriver.Visible = false;
            // 
            // btnScreenshotFull
            // 
            this.btnScreenshotFull.Location = new System.Drawing.Point(778, 2);
            this.btnScreenshotFull.Name = "btnScreenshotFull";
            this.btnScreenshotFull.Size = new System.Drawing.Size(59, 31);
            this.btnScreenshotFull.TabIndex = 10;
            this.btnScreenshotFull.Text = "Cap F";
            this.btnScreenshotFull.UseVisualStyleBackColor = true;
            this.btnScreenshotFull.Click += new System.EventHandler(this.btnScreenshot_Click);
            // 
            // btnScreenshotPrimary
            // 
            this.btnScreenshotPrimary.Location = new System.Drawing.Point(843, 3);
            this.btnScreenshotPrimary.Name = "btnScreenshotPrimary";
            this.btnScreenshotPrimary.Size = new System.Drawing.Size(59, 31);
            this.btnScreenshotPrimary.TabIndex = 11;
            this.btnScreenshotPrimary.Text = "Cap P";
            this.btnScreenshotPrimary.UseVisualStyleBackColor = true;
            this.btnScreenshotPrimary.Click += new System.EventHandler(this.btnScreenshotPrimary_Click);
            // 
            // btnScreenshotWindow
            // 
            this.btnScreenshotWindow.Location = new System.Drawing.Point(713, 3);
            this.btnScreenshotWindow.Name = "btnScreenshotWindow";
            this.btnScreenshotWindow.Size = new System.Drawing.Size(59, 31);
            this.btnScreenshotWindow.TabIndex = 12;
            this.btnScreenshotWindow.Text = "Cap W";
            this.btnScreenshotWindow.UseVisualStyleBackColor = true;
            this.btnScreenshotWindow.Click += new System.EventHandler(this.btnScreenshotWindow_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 480);
            this.Controls.Add(this.btnScreenshotWindow);
            this.Controls.Add(this.btnScreenshotPrimary);
            this.Controls.Add(this.btnScreenshotFull);
            this.Controls.Add(this.txtTestAppDriver);
            this.Controls.Add(this.btnTestAppDriver);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.statusInfo);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtDesFolder);
            this.Controls.Add(this.labSrcPath);
            this.Controls.Add(this.btnSrcFolder);
            this.Controls.Add(this.clbFiles);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "CpFiles";
            this.statusInfo.ResumeLayout(false);
            this.statusInfo.PerformLayout();
            this.ctmFiles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbFiles;
        private System.Windows.Forms.FolderBrowserDialog srcFolderPath;
        private System.Windows.Forms.Button btnSrcFolder;
        private System.Windows.Forms.Label labSrcPath;
        private System.Windows.Forms.TextBox txtDesFolder;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.StatusStrip statusInfo;
        private System.Windows.Forms.ToolStripStatusLabel statusCpInfo;
        private System.Windows.Forms.ContextMenuStrip ctmFiles;
        private System.Windows.Forms.ToolStripMenuItem menuClearContent;
        private System.Windows.Forms.ToolStripMenuItem menuCopyContentToClipBoard;
        private System.Windows.Forms.ToolStripMenuItem menuOpenFolderLocation;
        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.Button btnTestAppDriver;
        private System.Windows.Forms.TextBox txtTestAppDriver;
        private System.Windows.Forms.Button btnScreenshotFull;
        private System.Windows.Forms.Button btnScreenshotPrimary;
        private System.Windows.Forms.Button btnScreenshotWindow;
    }
}

