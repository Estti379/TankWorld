using TankWorld.Engine;
using TankWorld.Game.Items;

namespace TankWorld.Game.Events
{
    public class SceneStateEvent: Event
    {
        public enum Type
        {
            CHANGE_SCENE,
            FLIP_MENU,
            SPAWN_NEW_ENTITY,
            EXIT_GAME,
            DESPAWN_ENTITY,
            TANK_HIT,
            TIME_UP,
            SPAWN_GROUP
        }
        public readonly Type eventType;

        private Scene newScene;
        private GameObject sender;
        private GameObject target;
        private GameObject sniper;


        //Constructors

        public SceneStateEvent(Type eventType)
        {
            this.eventType = eventType;
        }
        public SceneStateEvent(Type eventType, Scene newScene)
        {
            this.eventType = eventType;
            this.newScene = newScene;
        }

        public SceneStateEvent(Type eventType, GameObject gameObject)
        {
            this.eventType = eventType;
            this.sender = gameObject;
        }
        public SceneStateEvent(Type eventType, TankObject target, TankObject sniper)
        {
            this.eventType = eventType;
            this.target = target;
            this.sniper = sniper;
        }

        //Accessors
        public Scene NewScene
        {
            set { newScene = value; }
            get { return newScene; }
        }

        public GameObject Sender
        {
            set { sender = value; }
            get { return sender; }
        }

        public GameObject Target { get => target; set => target = value; }
        public GameObject Sniper { get => sniper; set => sniper = value; }

        //Methods
    }
}
