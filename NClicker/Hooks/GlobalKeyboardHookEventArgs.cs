using System.ComponentModel;

namespace NClicker.Hooks
{
    public class GlobalKeyboardHookEventArgs : HandledEventArgs
    {
        public GlobalKeyboardHook.KeyboardState KeyboardState { get; }
        public GlobalKeyboardHook.LowLevelKeyboardInputEvent KeyboardData { get; }

        public GlobalKeyboardHookEventArgs(
            GlobalKeyboardHook.LowLevelKeyboardInputEvent keyboardData,
            GlobalKeyboardHook.KeyboardState keyboardState)
        {
            KeyboardData = keyboardData;
            KeyboardState = keyboardState;
        }
    }
}