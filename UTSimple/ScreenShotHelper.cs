using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTSimple
{
    //how to use 
    //var bounds = new Rectangle();
    //bounds = Screen.AllScreens.Aggregate(bounds, (current, screen)=> Rectangle.Union(current, screen.Bounds));
    //ScreenShotHelper.TakeAndSave(@"d:\screenshot.png", bounds, ImageFormat.Png);     

    public static  class ScreenShotHelper
    {
        private static Bitmap CopyFromScreen(Rectangle bounds)
        {
            try
            {
                var image = new Bitmap(bounds.Width, bounds.Height);
                var graphics = Graphics.FromImage(image);
                graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                return image;
            }
            catch (Win32Exception)
            {//When screen saver is active
                return null;
            }
        }

        public static Image Take(Rectangle bounds)
        {
            return CopyFromScreen(bounds);

        }

        public static byte[] TakeAsByteArray(Rectangle bounds)
        {
            var image = CopyFromScreen(bounds);
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        public static void TakeAndSave(string path, Rectangle bounds, ImageFormat imageFormat)
        {
            var image = CopyFromScreen(bounds);
            image.Save(path, imageFormat);
        }

        //method 2 

        public static void TakeScreenShot(string path , bool isCapturePrimaryScreenOnly) {
            // Determine the size of the "virtual screen", which includes all monitors.
            int screenLeft = SystemInformation.VirtualScreen.Left;
            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;

            
            if (isCapturePrimaryScreenOnly)
            {
                screenLeft = Screen.PrimaryScreen.Bounds.Left;
                screenTop = Screen.PrimaryScreen.Bounds.Top;
                screenWidth = Screen.PrimaryScreen.Bounds.Width;
                screenHeight = Screen.PrimaryScreen.Bounds.Height;

            }
            // Create a bitmap of the appropriate size to receive the screenshot.
            using (Bitmap bmp = new Bitmap(screenWidth, screenHeight))
            {
                // Draw the screenshot into our bitmap.
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                }

                string dirPath = AppDomain.CurrentDomain.BaseDirectory + "\\screenshots";
                if (Directory.Exists(dirPath) == false)
                    Directory.CreateDirectory(dirPath);

                //var screenshotPath = $@"path\to\screenshots\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                var screenshotPath = $@"{dirPath}\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

                // Do something with the Bitmap here, like save it to a file:
                bmp.Save(path, ImageFormat.Png);
            }
        }
    }
}
