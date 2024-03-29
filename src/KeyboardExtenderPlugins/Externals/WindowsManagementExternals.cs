﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;

        public static void ShowDesktop()
        {
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
        }
    

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);


        public static void RestoreAfterShowDesktop()
        {
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);

        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        public static IntPtr GetActiveWindowAndScreen(out int top, out int left, out int height, out int width)
        {
            IntPtr activeWindow = WindowsManagementExternals.GetForegroundWindow();
            var activeScr = Screen.FromHandle(activeWindow);
            top = activeScr.WorkingArea.Top;
            left = activeScr.WorkingArea.Left;
            height = activeScr.WorkingArea.Height;
            width = activeScr.WorkingArea.Width;
            return activeWindow;
        }

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
