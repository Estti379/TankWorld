namespace TankWorld.Engine
{
    public interface IObserver
    {
        void OnEvent(Event newEvent);
    }
}
