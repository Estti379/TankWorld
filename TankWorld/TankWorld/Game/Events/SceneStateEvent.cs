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
            SPAWN_PROJECTILE_ENTITY,
            EXIT_GAME,
            DESPAWN_PROJECTILE_ENTITY
        }
        public readonly Type eventType;

        private Scene newScene = null;
        private WeaponProjectileObject bullet = null;


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

        public SceneStateEvent(Type eventType, WeaponProjectileObject bullet)
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

        public WeaponProjectileObject Bullet
        {
            set { bullet = value; }
            get { return bullet; }
        }

        //Methods
    }
}
