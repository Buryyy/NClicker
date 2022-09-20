using NClicker.Services;
using System.ComponentModel;

namespace NClicker.Models
{
    public class GlobalKeyboardHookEventArgs : HandledEventArgs
    {
        public GlobalKeyboardService.KeyboardState KeyboardState { get; }
        public GlobalKeyboardService.LowLevelKeyboardInputEvent KeyboardData { get; }

        public GlobalKeyboardHookEventArgs(GlobalKeyboardService.LowLevelKeyboardInputEvent keyboardData,
            GlobalKeyboardService.KeyboardState keyboardState)
        {
            KeyboardData = keyboardData;
            KeyboardState = keyboardState;
        }
    }
}