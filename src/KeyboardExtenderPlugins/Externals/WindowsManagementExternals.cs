using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Avangarde.KeyboardExtenderPlugins.Externals
{
    public static class WindowsManagementExternals
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        public static void RestoreMaximizedActiveWindow()
        {
            IntPtr hwnd = GetForegroundWindow();
            ShowWindow(hwnd, ShowWindowCommands.SW_RESTORE);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        public static IntPtr FindWindow(string caption)
        {
            return FindWindow(null, caption);
        }


        public static void MaximizeActiveWindow()
        {
            IntPtr hwnd = GetForegroundWindow();
            ShowWindow(hwnd, ShowWindowCommands.SW_MAXIMIZE);
        }
    }

    public enum ShowWindowCommands
    {
        SW_MAXIMIZE = 3,
        SW_MINIMIZE = 6,
        SW_RESTORE = 9
    }
}
