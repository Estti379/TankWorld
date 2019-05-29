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
            EXIT_GAME,
            DESPAWN_BULLET_ENTITY
        }
        public readonly Type eventType;

        private Scene newScene = null;
        private BulletObject bullet = null;


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

        public SceneStateEvent(Type eventType, BulletObject bullet)
        {
            this.eventType = eventType;
            this.bullet = bullet;
        }

        //Accessors
        public Scene NewScene
        {
            set { newScene = value; }
            get { return newScene; }
        }

        public BulletObject Bullet
        {
            set { bullet = value; }
            get { return bullet; }
        }

        //Methods
    }
}
