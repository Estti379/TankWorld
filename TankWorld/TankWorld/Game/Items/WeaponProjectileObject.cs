using TankWorld.Engine;

namespace TankWorld.Game.Items
{
    abstract public class WeaponProjectileObject: GameObject, ICollide
    {
        //Constructors
        public WeaponProjectileObject() : base()
        {

        }

        

        //Accessors


        //Methods
        abstract public WeaponProjectileObject Clone();
        abstract override public void Render();
        abstract override public void Update(ref WorldItems world);
        public abstract HitBoxStruct GetHitBoxes();
        public abstract void CheckForCollision(ICollide collidingObject);
        public abstract void HandleCollision(ICollide collidingObject, Coordinate collisionPoint);
    }
}
