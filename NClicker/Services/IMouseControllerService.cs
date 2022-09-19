namespace NClicker.Services
{
    public interface IMouseControllerService
    {
        void LoopClick(int seconds, int milliseconds, int randomSeconds, int randomMilliseconds);

        void Stop();
    }
}