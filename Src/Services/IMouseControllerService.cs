namespace NClicker.Services
{
    public interface IMouseControllerService
    {
        void OnLoopClick(int seconds, int milliseconds, int randomSeconds, int randomMilliseconds);

        void Stop();
    }
}