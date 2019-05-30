using TankWorld.Engine;

namespace TankWorld.Game.Items
{
    abstract public class WeaponProjectileObject: GameObject
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
    }
}
