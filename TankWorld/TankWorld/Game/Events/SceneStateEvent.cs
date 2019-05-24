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
            SPAWN_BULLET_ENTITY,
            EXIT_GAME
        }
        public readonly Type eventType;

        private Scene newScene = null;
        private BulletObject newBullet = null;


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

        public SceneStateEvent(Type eventType, BulletObject newBullet)
        {
            this.eventType = eventType;
            this.newBullet = newBullet;
        }

        //Accessors
        public Scene NewScene
        {
            set { newScene = value; }
            get { return newScene; }
        }

        public BulletObject NewBullet
        {
            set { newBullet = value; }
            get { return newBullet; }
        }

        //Methods
    }
}
