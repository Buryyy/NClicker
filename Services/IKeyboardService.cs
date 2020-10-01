namespace NClicker.Services
{
    public interface IKeyboardService
    {
        /// <summary>
        /// Block/unblock user input.
        /// </summary>
        /// <param name="blocked"></param>
        void BlockInput(bool blocked);
    }
}