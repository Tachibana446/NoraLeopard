using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendTextToVoiceroid
{
    class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        static void Main(string[] args)
        {

            foreach (var p in System.Diagnostics.Process.GetProcesses())
            {
                if (Regex.IsMatch(p.ProcessName, @"VoiceroidEditor"))
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    SendKeys.SendWait("^a");
                    SendKeys.SendWait("^v");
                    SendKeys.SendWait("{F5}");
                    break;
                }
            }
        }
    }
}
