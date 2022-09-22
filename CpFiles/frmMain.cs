using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTSimple;

namespace CpFiles
{
    public partial class frmMain : Form
    {
        

        List<string> initCheckedItems = new List<string> { "Config.json", "DailyTotal.dat", "Journal.dat" ,"CancelMode.chk",
                                                            "LastOutputId.dat","LastSettlementBrandCode.dat","LastSettlementSeqNo.dat",
                                                            "LastTransactionInquiry.dat","SettlementSeqNo.dat"};

        //Appium Driver URL it works like a windows Service on your PC  
        private const string appiumDriverURI = "http://127.0.0.1:4723";
        //Application Key of your UWA   
        //U can use any .Exe file as well for open a windows Application  
        private const string calApp = "Saturn1000LaneIF.Test.exe";
        protected static WindowsDriver<WindowsElement> calSession;

        public frmMain()
        {
            InitializeComponent();
            labSrcPath.Text = Properties.Resources.Src_Folder_Path;
            LoadFilesToListCheckBox(labSrcPath.Text);
            statusCpInfo.Text = "";
            this.clbFiles.MouseUp += new MouseEventHandler(CpFilesItem_RightClick);
        }

        private void CpFilesItem_RightClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int index = this.clbFiles.IndexFromPoint(e.Location);
                //set selected item
                clbFiles.SelectedIndex = index;
                //Console.WriteLine(index);
                //FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;

                //int index = this.clbFiles.IndexFromPoint(e.Location);
                ctmFiles.Show(this.clbFiles, new Point(e.X, e.Y));

            }
        }

        private void btnSrcFolder_Click(object sender, EventArgs e)
        {
            statusCpInfo.Text = "";
            labSrcPath.Text = "";
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Properties.Resources.Src_Folder_Path;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    labSrcPath.Text = fbd.SelectedPath;

                    // string[] files = Directory.GetFiles(fbd.SelectedPath);

                    LoadFilesToListCheckBox(fbd.SelectedPath);
                }
            }

            
        }

        private void LoadFilesToListCheckBox(string FolderPath)
        {
            statusCpInfo.Text = "";
            clbFiles.Items.Clear();

            bool exists = System.IO.Directory.Exists(FolderPath);

            if (!exists)
                return;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderPath);
            FileSystemInfo[] allFiles = di.GetFiles("*.*", SearchOption.AllDirectories);

            //FileSystemInfo[] files = di.GetFileSystemInfos();
            foreach (FileSystemInfo file in allFiles)
            {
                if (file.FullName.ToLower().Contains("snd") 
                    || file.FullName.ToLower().Contains("img") 
                    || file.FullName.ToLower().Contains("_bk")
                    || file.FullName.ToLower().Contains("DailyTotal_Backup".ToLower())
                    || file.FullName.ToLower().Contains("Journal_Backup".ToLower())
                    || file.FullName.ToLower().Contains(".exe")
                    || file.FullName.ToLower().Contains(".bmp")
                    || file.FullName.ToLower().Contains("log4net.config")
                )
                {
                }
                else
                {
                    clbFiles.Items.Add(file);
                    //clbFiles.Items.AddRange(allFiles); 
                }
            }

            //init checked item 
            for (int i = 0; i < clbFiles.Items.Count; i++)
            {
                //Console.WriteLine(clbFiles.Items[i].ToString());
                if (initCheckedItems.Contains(clbFiles.Items[i].ToString()))
                    clbFiles.SetItemChecked(i, true);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            statusCpInfo.Text = "";
            try
            {
                //create folder 
                bool exists = System.IO.Directory.Exists(txtDesFolder.Text);
                if (!exists)
                    System.IO.Directory.CreateDirectory(txtDesFolder.Text);

                var checkedItems = clbFiles.CheckedItems;

                foreach (FileSystemInfo item in checkedItems)
                {
                    //copy file
                    File.Copy(item.FullName, txtDesFolder.Text + "\\" + Path.GetFileName(item.FullName), true);

                }

                statusCpInfo.Text = "Copy files ok";
            }
            catch (Exception ex)
            {
                statusCpInfo.Text = "Copy files fail";
            }
            
        }

        private void txtDesFolder_DoubleClick(object sender, EventArgs e)
        {
            //create folder if not existed
            bool exists = System.IO.Directory.Exists(txtDesFolder.Text);
            if (!exists)
                System.IO.Directory.CreateDirectory(txtDesFolder.Text);

            if (txtDesFolder.Text!="" && System.IO.Directory.Exists(txtDesFolder.Text))
                Process.Start(txtDesFolder.Text);


        }

        private void clbFiles_DoubleClick(object sender, EventArgs e)
        {
            if (clbFiles.SelectedItem != null)
            {
                FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;

                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "notepad.exe"; 
                myProcess.StartInfo.Arguments = item.FullName;
                myProcess.Start();
            }
        }

        private void menuClearContent_Click(object sender, EventArgs e)
        {
            FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;
            System.IO.File.WriteAllText(item.FullName, string.Empty);
        }

        private void menuCopyContentToClipBoard_Click(object sender, EventArgs e)
        {
            FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;
            //var content = File.ReadAllText(item.FullName);
            //using (StreamReader sr = new StreamReader(item.FullName,Encoding.Default))
            using (StreamReader sr = new StreamReader(File.OpenRead(item.FullName), Encoding.Default))
            {
                String text = sr.ReadToEnd();
                if(text != string.Empty)
                    Clipboard.SetText(text);
            }

            //Clipboard.SetText(content);
        }

        private void menuOpenFolderLocation_Click(object sender, EventArgs e)
        {
            FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;
            string folder = Path.GetDirectoryName(item.FullName);


            if (folder != "" && System.IO.Directory.Exists(folder))
                Process.Start(folder);
        }

        private void txtDesFolder_TextChanged(object sender, EventArgs e)
        {

        }

        public  void WatchFile(string filePath)
        {
            string folder = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileName(filePath);
            if (folder != "" && System.IO.Directory.Exists(folder))
            {
                var watch = new FileSystemWatcher();
                watch.Path = folder;
                watch.Filter = fileName;
                watch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite; //more options
                watch.Changed += new FileSystemEventHandler(OnContentFileChanged);
                watch.EnableRaisingEvents = true;
            }

        }

        /// Functions:
        private  void OnContentFileChanged(object source, FileSystemEventArgs e)
        {
            //if (e.FullPath == @"D:\tmp\file.txt")
            //{
            //    // do stuff
            //}

            //display content into textbox 
            FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;
            //chi xu ly file duoc chon
            if (e.FullPath != item.FullName)
            {
                return;
            }

                ControlExtension.SetPropertyThreadSafe(this.txtContent, () => this.txtContent.Text, "");
            var fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
            {
                String text = sr.ReadToEnd();
                if (text != string.Empty)
                {
                    ControlExtension.SetPropertyThreadSafe(this.txtContent , ()=> this.txtContent.Text , text);

                }
                    
            }

        }

        private void clbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileSystemInfo item = (FileSystemInfo)clbFiles.SelectedItem;
            txtContent.Text =String.Empty;

            if (item != null)
            {
                //display content to textbox .
                //using (StreamReader sr = new StreamReader(item.FullName, Encoding.Default))
                var fs = new FileStream(item.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    String text = sr.ReadToEnd();
                    if (text != string.Empty)
                    {
                        txtContent.Text = text;

                    }
                }

                WatchFile(item.FullName);
            }
        }

        [Obsolete]
        private void btnTestAppDriver_Click(object sender, EventArgs e)
        {

            //DesiredCapabilities appCapabilities = new DesiredCapabilities();
            //appCapabilities.SetCapability("app", calApp);
            //appCapabilities.SetCapability("deviceName", "WindowsPC");
            //Create a session to intract with Calculator windows application  
            //calSession = new WindowsDriver<WindowsElement>(new Uri(appiumDriverURI), appCapabilities);

           ControlExtension.LeftMouseClick(487, 217);
            SendKeys.SendWait("111");
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            //screenshoot 
            string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\screenshots";
            if (Directory.Exists(dirPath) == false)
                Directory.CreateDirectory(dirPath);

            //var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            //_EmoneyTestDriver.GetScreenshot().SaveAsFile(screenshotPath);

            ScreenShotHelper.TakeScreenShot(screenshotPath, false);

            Bitmap myBitmap = Ultilities.FromFile(screenshotPath);

            Graphics g = Graphics.FromImage(myBitmap);

            g.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") , new Font("Tahoma",40) ,Brushes.Orange , new Point(10,10));

            myBitmap.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);

            Ultilities.ImageFileToClipboard(screenshotPath);
        }

        private void btnScreenshotPrimary_Click(object sender, EventArgs e)
        {
            //screenshoot 
            string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\screenshots";
            if (Directory.Exists(dirPath) == false)
                Directory.CreateDirectory(dirPath);

            //var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            //_EmoneyTestDriver.GetScreenshot().SaveAsFile(screenshotPath);

            ScreenShotHelper.TakeScreenShot(screenshotPath, true);

            Ultilities.ImageFileToClipboard(screenshotPath);
        }


        
        private void btnScreenshotWindow_Click(object sender, EventArgs e)
        {
            ////screenshoot 
            //IntPtr windowHandle = new IntPtr();

            //windowHandle= Ultilities.GetHandleWindowByProcessName("WPSaturnEMoney");

            //string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\screenshots";
            //if (Directory.Exists(dirPath) == false)
            //    Directory.CreateDirectory(dirPath);

            ////var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            //var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            ////_EmoneyTestDriver.GetScreenshot().SaveAsFile(screenshotPath);

            //if (windowHandle != IntPtr.Zero)
            //{
               
            //    Bitmap myBitmap = Ultilities.CaptureWindow(windowHandle);

            //    Graphics g = Graphics.FromImage(myBitmap);

            //    g.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), new Font("Tahoma", 40), Brushes.Orange, new Point(10, 10));

            //    myBitmap.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);

            //    Ultilities.ImageFileToClipboard(screenshotPath);

            //}
            
            new SelectArea().Show();
        }
    }
}
