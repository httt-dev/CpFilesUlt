using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace UTSimple
{
    [TestClass]
    public class HEI_SELF_82_001_01
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, uint windowStyle);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        //HEI_SELF-82 001 POSレジアプリ連携－アプリ起動 -実施内容1（カスタマー画面ON）
        //Appium Driver URL it works like a windows Service on your PC  
        private const string appiumDriverURI = "http://127.0.0.1:4723";
        //Application Key of your UWA   
        //U can use any .Exe file as well for open a windows Application  
        private const string calApp = "Saturn1000LaneIF";

        protected static WindowsDriver<WindowsElement> _IFTestDriver;
        protected static WindowsDriver<WindowsElement> _EmoneyTestDriver;
        protected static WindowsDriver<WindowsElement> _POSDriver;

        //Start Transaction Button Posistion
        private static int PosStartTransactionBtnPosX = 249;
        private static int PosStartTransactionBtnPosY = 369;

        //location text position
        private static int PosLocationPosX = 458;
        private static int PosLocationPosY = 210;

        [TestInitialize]
        public void TestInitialize()
        {
            Console.WriteLine("Inside TestInitialize");

            if (_IFTestDriver == null)
            {
                BeforeEach();
            }

        }

        [TestMethod]
        public void Test_StartUp_OK_Message_Check()
        {
            Console.WriteLine("Inside TestMethod Test_StartUp_OK_Message_Check");

            Thread.Sleep(4000);
            string caption = "";

            if (_EmoneyTestDriver != null)
            {

                //screenshoot 
                string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\screenshots";
                if (Directory.Exists(dirPath) == false)
                    Directory.CreateDirectory(dirPath);

                //var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                //_EmoneyTestDriver.GetScreenshot().SaveAsFile(screenshotPath);

                ScreenShotHelper.TakeScreenShot(screenshotPath , true);
                //var windowCount = _EmoneyTestDriver.WindowHandles.Count;
                //if (windowCount > 1)
                //    _EmoneyTestDriver.SwitchTo().Window(_EmoneyTestDriver.WindowHandles[0]);

                //var elements = _EmoneyTestDriver.FindElementsByWindowsUIAutomation("Message");
                var element= _EmoneyTestDriver.FindElementByAccessibilityId("Message");

                if (element != null)
                {
                    caption = element.Text;
                }

                //ButtonMain
                element = _EmoneyTestDriver.FindElementByAccessibilityId("ButtonMain");
                if (element != null)
                {
                    if( element.Text == "OK")
                    {
                        element.Click();
                    }
                }

            }
            Assert.AreEqual("アプリケーションは正常に起動しました。", caption);
        }

      
        public void Test_Start_Transaction_Check()
        {
            Console.WriteLine("Inside TestMethod Test_Start_Transaction_Check");

            Thread.Sleep(4000);
            string caption = "";

            if (_EmoneyTestDriver != null)
            {

                //screenshoot 
                string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\screenshots";
                if (Directory.Exists(dirPath) == false)
                    Directory.CreateDirectory(dirPath);

                //var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                _EmoneyTestDriver.GetScreenshot().SaveAsFile(screenshotPath);

                //var windowCount = _EmoneyTestDriver.WindowHandles.Count;
                //if (windowCount > 1)
                //    _EmoneyTestDriver.SwitchTo().Window(_EmoneyTestDriver.WindowHandles[0]);

                //var elements = _EmoneyTestDriver.FindElementsByWindowsUIAutomation("Message");
                var element = _EmoneyTestDriver.FindElementByAccessibilityId("Message");

                if (element != null)
                {
                    caption = element.Text;
                }

                //ButtonMain
                element = _EmoneyTestDriver.FindElementByAccessibilityId("ButtonMain");
                if (element != null)
                {
                    if (element.Text == "OK")
                    {
                        element.Click();
                    }
                }

            }
            Assert.AreEqual("アプリケーションは正常に起動しました。", caption);
        }


        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("Inside TestCleanup");
            //Copy file 
            //log file 
            string DesFolderPath  = @"D:\workspace\Hei\UT\HEI_SELF-files-Common_iD\HEI_SELF-82 001 POSレジアプリ連携－アプリ起動\実施内容1（カスタマー画面ON）";

            Ultilities.CopyFile(AppConstants.SrcBaseFolderPath + "\\" + AppConstants.SrcLogFolderPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log", DesFolderPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");

            //CloseApp(_IFTestDriver);
            CloseApp(_EmoneyTestDriver);

            
        }

        private static WindowsDriver<WindowsElement> grabWindowsProcesses(string processName= "Saturn1000LaneIF")
        {
            int number = 0;
            WindowsDriver<WindowsElement> _driver;

            IntPtr myAppTopLevelWindowHandle = new IntPtr();
            foreach (Process clsProcess in Process.GetProcesses())
            {
                number++;

                //_txtBxOutput.Text += number.ToString() + ") " + clsProcess.ProcessName + "\n";
                if (clsProcess.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    myAppTopLevelWindowHandle = clsProcess.MainWindowHandle;
                    break;
                }
            }

            if (myAppTopLevelWindowHandle != null)
            {
                ShowWindow(myAppTopLevelWindowHandle, 1u);
                //SetWindowPos(myAppTopLevelWindowHandle, new IntPtr(-1), 0, 0, 0, 0, 3u);
            }

            var appTopLevelWindowHandleHex = myAppTopLevelWindowHandle.ToString("x");

            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("appTopLevelWindow", appTopLevelWindowHandleHex);

            /*
			 * Error I get here is: OpenQA.Selenium.WebDriverException: 'b2c is not a top level window handle'
			 */

            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);

            //Console.WriteLine(saturn1000LaneIFSession.FindElementByAccessibilityId("txtSendMessage").Text);

            return _driver;
        }

        private void CloseApp(WindowsDriver<WindowsElement> _driver)
        {
            var windowCount = _driver.WindowHandles.Count;
            if (_driver != null && windowCount > 1)
            {
                Ultilities.KillApp("WPSaturnEMoney");
                throw new IOException($"Ensure only the main window is open at the end of each test. There are currently {windowCount} windows open");
                
            }

            _driver.Quit();
        }

        public void BeforeEach()
        {
            // other test setup stuff

            //_driver = new WindowsDriver("http://127.0.0.1:4723", appOptions);

            _IFTestDriver = grabWindowsProcesses("Saturn1000LaneIF");


            _POSDriver = grabWindowsProcesses("POS_INTERFACE_TEST");

            Ultilities.KillApp("WPSaturnEMoney");
            //_IFTestDriver = grabWindowsProcesses("Saturn1000LaneIF");

            Thread.Sleep(1000);
            //Close app
            Ultilities.LeftMouseClick(239,324);
            Thread.Sleep(1000);
            //Open app button
            Ultilities.LeftMouseClick(112, 324);

            //Input X position of External app
            Ultilities.LeftMouseClick(742, 81);
            SendKeys.SendWait("{DELETE}");
            SendKeys.SendWait("{DELETE}");
            SendKeys.SendWait("{DELETE}");
            SendKeys.SendWait("0");

            //Input Y position of External app
            Ultilities.LeftMouseClick(806, 76);
            SendKeys.SendWait("{DELETE}");
            SendKeys.SendWait("{DELETE}");
            SendKeys.SendWait("{DELETE}");
            SendKeys.SendWait("380");

            //Click ExApp Show
            Ultilities.LeftMouseClick(787, 54);

            _EmoneyTestDriver = grabWindowsProcesses("WPSaturnEMoney");

            var windowCount = _EmoneyTestDriver.WindowHandles.Count;
            if (windowCount > 1)
                _EmoneyTestDriver.SwitchTo().Window(_EmoneyTestDriver.WindowHandles[0]);

            //_driver.SwitchTo().Window(_driver.WindowHandles[0]);
        }

        



    }
}
