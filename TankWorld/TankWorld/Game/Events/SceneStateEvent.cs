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
            DESPAWN_ENTITY
        }
        public readonly Type eventType;

        private Scene newScene;
        private GameObject sender;


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

        //Methods
    }
}
