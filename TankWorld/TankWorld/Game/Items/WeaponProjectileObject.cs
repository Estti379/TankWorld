using TankWorld.Engine;

namespace TankWorld.Game.Items
{
    abstract public class WeaponProjectileObject: IRender
    {
        //Constructors
        public WeaponProjectileObject()
        {

        }

        //Accessors


        //Methods
        abstract public WeaponProjectileObject Clone();

        abstract public void Render();

        abstract public void Update(ref WorldItems world);
    }
}
