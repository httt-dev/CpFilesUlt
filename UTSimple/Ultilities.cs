using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTSimple
{
    public static class Ultilities
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSE_LEFTDOWN = 0x02;
        public const int MOUSE_LEFTUP = 0x04;

        private delegate void SetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetClientRect(IntPtr hWnd, ref Rect rect);

        [DllImport("user32.dll")]
        private static extern IntPtr ClientToScreen(IntPtr hWnd, ref Point point);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        public static void SetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
        {
            var propertyInfo = (property.Body as MemberExpression).Member
                as PropertyInfo;

            if (propertyInfo == null ||
                !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) ||
                @this.GetType().GetProperty(
                    propertyInfo.Name,
                    propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (@this.InvokeRequired)
            {
                @this.Invoke(new SetPropertyThreadSafeDelegate<TResult>
                (SetPropertyThreadSafe),
                new object[] { @this, property, value });
            }
            else
            {
                @this.GetType().InvokeMember(
                    propertyInfo.Name,
                    BindingFlags.SetProperty,
                    null,
                    @this,
                    new object[] { value });
            }
        }

        public static void KillApp(string processName)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //_txtBxOutput.Text += number.ToString() + ") " + clsProcess.ProcessName + "\n";
                if (clsProcess.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    clsProcess.Kill();
                    //break;
                }
            }
        }

        public static void LeftMouseClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSE_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_LEFTUP, x, y, 0, 0);
        }

        public static bool CopyFile(string srcFilePath , string desFilePath)
        {
            try
            {
                if (File.Exists(srcFilePath) == false)
                    return false;

                bool exists = Directory.Exists(desFilePath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(desFilePath);


                //copy file
                File.Copy(srcFilePath, desFilePath, true);

                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
           
        }

        public static bool DelFile(string filePathd)
        {
            try
            {
                if (File.Exists(filePathd) == false)
                    return false;

                
                //copy file
                File.Delete(filePathd);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }

        public static void ImageFileToClipboard(string imgFilePath)
        {
            Clipboard.SetImage(Image.FromFile(imgFilePath));

            ///Or
            //byte[] bytes = File.ReadAllBytes(imgFilePath);
            //Image img;
            //using (var ms = new MemoryStream(bytes))
            //    img = Image.FromStream(ms);
            //Clipboard.SetImage(img);

            ////OR
            //var list = new StringCollection();
            //list.Add(imgFilePath);
            //Clipboard.SetFileDropList(list);

        }

        public static Bitmap FromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            //return img;

            //bitmap return case
            return (Bitmap)Image.FromStream(ms);
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetClientRect(handle, ref rect);

            var point = new Point(0, 0);
            ClientToScreen(handle, ref point);

            var bounds = new Rectangle(point.X, point.Y, rect.Right*2, rect.Bottom);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                IntPtr dc = graphics.GetHdc();
                bool success = PrintWindow(handle, dc, 0);
                graphics.ReleaseHdc(dc);
            }

            return result;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static IntPtr GetHandleWindowByProcessName(string processName)
        {
            IntPtr windowHandle = new IntPtr();
            //Process[] processes = Process.GetProcessesByName(appName);

            //foreach (Process p in processes)
            //{
            //    windowHandle = p.MainWindowHandle;
            //}

            foreach (Process clsProcess in Process.GetProcesses())
            {
                //_txtBxOutput.Text += number.ToString() + ") " + clsProcess.ProcessName + "\n";
                if (clsProcess.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    windowHandle = clsProcess.MainWindowHandle;
                    break;
                }
            }

            return windowHandle;
        }

    }
}
