using NClicker.Models;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace NClicker.Services
{
    public class MouseControllerService : IService
    {
        public MouseControllerService()
        {
            _random = new Random();
        }

        private readonly Random _random;

        public bool IsRunning { get; set; }
        private static Point CursorPosition => GetCursorPos(out var position) ? position : new Point(-1, -1);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwsFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Position lpPoint);

        public void LoopClick(int seconds, int milliseconds, int randomSeconds, int randomMilliseconds)
        {
            if (IsRunning) return;
            Task.Run(async () =>
            {
                IsRunning = true;

                while (IsRunning)
                {
                    int x = Convert.ToInt32(CursorPosition.X);
                    int y = Convert.ToInt32(CursorPosition.Y);

                    if (x == -1 || y == -1)
                    {
                        return;
                    }
                    mouse_event((int)MouseClickType.LeftDown, x, y, 0, 0);
                    mouse_event((int)MouseClickType.LeftUp, x, y, 0, 0);

                    await Task.Delay(new TimeSpan(
                        0, 0, 0, seconds + _random.Next(0 + randomSeconds),
                        milliseconds + _random.Next(0, randomMilliseconds)));
                }
            }).ConfigureAwait(false);
        }

        public void Stop()
        {
            IsRunning = false;
        }

       
    }
}