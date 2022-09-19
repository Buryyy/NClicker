using NClicker.Models;
using System.Runtime.InteropServices;

namespace NClicker.Helpers
{
    public class Win32UserApi
    {

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void OnMouseClick(int dwsFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMousePosition(out Position lpPoint);
    }
}
