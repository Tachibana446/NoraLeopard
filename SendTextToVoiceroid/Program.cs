using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            bool isFoundGameProcess = false;
            bool isPlayedVoice = false;
            Process game = null;

            foreach (var p in System.Diagnostics.Process.GetProcesses())
            {
                if (!isFoundGameProcess && p.ProcessName == "program" && p.MainWindowTitle == "VoiceToText")
                {
                    game = p;
                    isFoundGameProcess = true;
                }

                if (!isPlayedVoice && Regex.IsMatch(p.ProcessName, @"VoiceroidEditor"))
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    SendKeys.SendWait("^a");
                    SendKeys.SendWait("^v");
                    SendKeys.SendWait("{F5}");
                    isPlayedVoice = true;
                }

                if (isFoundGameProcess && isPlayedVoice)
                {
                    SetForegroundWindow(game.MainWindowHandle);
                    break;
                }
            }
        }
    }
}
