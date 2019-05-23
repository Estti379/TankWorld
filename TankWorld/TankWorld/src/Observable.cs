using System.Collections.Generic;

namespace TankWorld.src
{
    public class Observable
    {
        List<IObserver> observers;

        //Constructors
        public Observable()
        {
            observers = new List<IObserver>();
        }

        //Accessors

        //Methods

        public void AddObserver(IObserver newObserver)
        {
            observers.Add(newObserver);
        }

        public void Notify(Event newEvent)
        {
            foreach (IObserver entry in observers)
            {
                entry.OnEvent(newEvent);
            }
        }
    }
}
