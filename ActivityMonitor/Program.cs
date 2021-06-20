using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ActivityMonitor
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        static void Main(string[] args)
        {
            string activeWindowTitle, previousActiveWindowTitle = "";
            activeWindowTitle = GetActiveWindowTitle();
            previousActiveWindowTitle = activeWindowTitle;

            DateTime activatedWindowDateTime = DateTime.Now;

            Console.WriteLine($"Initial active window: {activeWindowTitle}\nActivated at: {activatedWindowDateTime.ToString("yyyy-MM-dd HH:mm:ss")}");

            while (true)
            {
                activeWindowTitle = GetActiveWindowTitle();
                Debug.WriteLine(activeWindowTitle);
                if (!string.IsNullOrEmpty(activeWindowTitle) && previousActiveWindowTitle != activeWindowTitle)
                {
                    DateTime windowActiveEndDateTime = DateTime.Now;
                    TimeSpan windowActiveTimeSpan = windowActiveEndDateTime - activatedWindowDateTime;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Active window switched to: {activeWindowTitle}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Activated at: { activatedWindowDateTime.ToString("yyyy-MM-dd HH:mm:ss")}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Switched at: { windowActiveEndDateTime}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Previous window active time: { windowActiveTimeSpan}");

                    previousActiveWindowTitle = activeWindowTitle;
                    activatedWindowDateTime = DateTime.Now;
                }
            }
        }

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}
