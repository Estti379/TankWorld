namespace TankWorld.Engine
{
    public static class MainEventBus
    {
        static Observable dispatcher = new Observable();
        //Accessors

        //Methods
        static public void Register(IObserver newObserver)
        {
            dispatcher.AddObserver(newObserver);
        }
        static public void PostEvent(Event newEvent)
        {
            dispatcher.Notify(newEvent);
        }
    }
}
