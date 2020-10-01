using Autofac;
using NClicker.Hooks;
using NClicker.ViewModels;

namespace NClicker.Services
{
    public class KeyboardService : IKeyboardService
    {
        private const int VirtualKeyF1 = 0x70;
        private const int VirtualKeyF2 = 0x71;

        private readonly GlobalKeyboardHook _globalKeyboardHook;
        private bool _blockKeys;

        public KeyboardService(GlobalKeyboardHook globalKeyboardHook)
        {
            _globalKeyboardHook = globalKeyboardHook;
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs @event)
        {
            switch (@event.KeyboardData.VirtualCode)
            {
                case VirtualKeyF1:
                    App.Context.Resolve<MainViewModel>().StartClickCommand.Execute(null);
                    @event.Handled = _blockKeys;
                    break;

                case VirtualKeyF2:
                    App.Context.Resolve<MainViewModel>().StopClickCommand.Execute(null);
                    @event.Handled = _blockKeys;
                    break;

                default: break;
            }
        }

        public void BlockInput(bool blocked)
        {
            _blockKeys = blocked;
        }
    }
}