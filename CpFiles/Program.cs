using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


[assembly: CLSCompliant(true)]
namespace CpFiles
{
    internal static class Program
    {
		// file type to register
		//const string FileType = "jpegfile";
		const string FileType = "txtfile";

		// context menu name in the registry
		const string KeyName = "Simple Context Menu";

		// context menu text
		const string MenuText = "Copy content with default encoding";


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main(string[] args)
        {
			//grabWindowsProcesses();

			if (args.Length == 0)
            {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new frmMain());
            }
            else
            {
				// process register or unregister commands
				if (!ProcessCommand(args))
				{
					// invoked from shell, process the selected file
					//CopyGrayscaleImage(args[0]);
					CopyToClipboardWithDefaultEncoding(args[0]);
				}
			}
			

		}

		/// <summary>
		/// Process command line actions (register or unregister).
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		/// <returns>True if processed an action in the command line.</returns>
		static bool ProcessCommand(string[] args)
		{
			// register
			if (args.Length == 0 || string.Compare(args[0], "-register", true) == 0)
			{
				// full path to self, %L is placeholder for selected file
				string menuCommand = string.Format(
					"\"{0}\" \"%L\"", Application.ExecutablePath);

				// register the context menu
				FileShellExtension.Register(Program.FileType,
					Program.KeyName, Program.MenuText,
					menuCommand);

				MessageBox.Show(string.Format(
					"The {0} shell extension was registered.",
					Program.KeyName), Program.KeyName);

				return true;
			}

			// unregister		
			if (string.Compare(args[0], "-unregister", true) == 0)
			{
				// unregister the context menu
				FileShellExtension.Unregister(Program.FileType, Program.KeyName);

				MessageBox.Show(string.Format(
					"The {0} shell extension was unregistered.",
					Program.KeyName), Program.KeyName);

				return true;
			}

			// command line did not contain an action
			return false;
		}

		static void CopyToClipboardWithDefaultEncoding(string filePath)
		{
			try
			{
				if (filePath == null)
					return;
				if (File.Exists(filePath))
				{
					var fileContent = string.Empty;
					using (var streamReader = new StreamReader(filePath, System.Text.Encoding.Default))
					{
						fileContent = streamReader.ReadToEnd();
						if (fileContent != String.Empty)
						{
							Clipboard.SetText(fileContent);

							//update other process
							grabWindowsProcesses();
                            if (saturn1000LaneIFSession != null)
                            {
								//MessageBox.Show(fileContent);
								//saturn1000LaneIFSession.FindElementByAccessibilityId("txtSendMessage").Execute("setValue", new Dictionary<string, object>
								//{
								//	["id"] = "txtSendMessage",
								//	["value"] = fileContent
								//});
								//saturn1000LaneIFSession.FindElementByAccessibilityId("txtSendMessage").SetImmediateValue(fileContent);
								var element = saturn1000LaneIFSession.FindElementByAccessibilityId("txtSendMessage");
								if(element != null)
                                {
									element.Click();
									element.Clear();
									SendKeys.SendWait("^v");
									element.Click();

									//screenshoot 
									string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\screenshots";
									if(Directory.Exists(dirPath)==false)
										Directory.CreateDirectory(dirPath);

									//var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
									var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
									saturn1000LaneIFSession.GetScreenshot().SaveAsFile(screenshotPath);
								}
								//Actions action = new Actions(saturn1000LaneIFSession);
								//action.SendKeys(fileContent);
								//action.SendKeys(saturn1000LaneIFSession.FindElementByAccessibilityId("txtSendMessage"), fileContent);
								// action.Perform();
							}
						}
					}

				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("An error occurred: {0}", ex.Message), Program.KeyName);
				return;
			}
		}
	    static WindowsDriver<WindowsElement> saturn1000LaneIFSession;
		private static WindowsDriver<WindowsElement> grabWindowsProcesses()
		{
			int number = 0;

			IntPtr myAppTopLevelWindowHandle = new IntPtr();
			foreach (Process clsProcess in Process.GetProcesses())
			{
				number++;

				//_txtBxOutput.Text += number.ToString() + ") " + clsProcess.ProcessName + "\n";
				if (clsProcess.ProcessName.Contains("Saturn1000LaneIF"))
				{
					myAppTopLevelWindowHandle = clsProcess.MainWindowHandle;
					break;
				}
			}

			var appTopLevelWindowHandleHex = myAppTopLevelWindowHandle.ToString("x");

			AppiumOptions appCapabilities = new AppiumOptions();
			appCapabilities.AddAdditionalCapability("platformName", "Windows");
			appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
			appCapabilities.AddAdditionalCapability("appTopLevelWindow", appTopLevelWindowHandleHex);

			/*
			 * Error I get here is: OpenQA.Selenium.WebDriverException: 'b2c is not a top level window handle'
			 */
			
			saturn1000LaneIFSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);

			//Console.WriteLine(saturn1000LaneIFSession.FindElementByAccessibilityId("txtSendMessage").Text);

			return saturn1000LaneIFSession;
		}
		/// <summary>
		/// Make a grayscale copy of the image.
		/// </summary>
		/// <param name="filePath">Full path to the image to copy.</param>
		static void CopyGrayscaleImage(string filePath)
		{
			try
			{
				// full path to the grayscale copy
				string grayFilePath = Path.Combine(
					Path.GetDirectoryName(filePath),
					string.Format("{0} (grayscale){1}",
					Path.GetFileNameWithoutExtension(filePath),
					Path.GetExtension(filePath)));

				// using calls Dispose on the objects, important 
				// so the file is not locked when the app terminates
				using (Image image = new Bitmap(filePath))
				using (Bitmap grayImage = new Bitmap(image.Width, image.Height))
				using (Graphics g = Graphics.FromImage(grayImage))
				{
					// setup grayscale matrix
					ImageAttributes attr = new ImageAttributes();
					attr.SetColorMatrix(new ColorMatrix(new float[][]{
						new float[]{0.3086F,0.3086F,0.3086F,0,0},
						new float[]{0.6094F,0.6094F,0.6094F,0,0},
						new float[]{0.082F,0.082F,0.082F,0,0},
						new float[]{0,0,0,1,0,0},
						new float[]{0,0,0,0,1,0},
						new float[]{0,0,0,0,0,1}}));

					// create the grayscale image
					g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
						0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attr);

					// save to the file system
					grayImage.Save(grayFilePath, ImageFormat.Jpeg);

					// success
					MessageBox.Show(string.Format("Copied grayscale image {0}", grayFilePath), Program.KeyName);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("An error occurred: {0}", ex.Message), Program.KeyName);
				return;
			}
		}


	}
}
