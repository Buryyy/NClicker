using NClicker.Helpers;
using NClicker.Models;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NClicker.Services
{
    public class MouseControllerService : IMouseControllerService
    {
        private readonly Random _random;

        public bool IsRunning { get; set; }

        public MouseControllerService(Random random)
        {
            _random = new Random();
        }



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
                    if (!Win32UserApi.GetMousePosition(out var coords))
                    {
                        return;
                    }
                    int x = Convert.ToInt32(coords.X);
                    int y = Convert.ToInt32(coords.Y);

                    Win32UserApi.OnMouseClick((int)NativeMouseClickFlags.LeftDown, x, y, 0, 0);
                    Win32UserApi.OnMouseClick((int)NativeMouseClickFlags.LeftUp, x, y, 0, 0);

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