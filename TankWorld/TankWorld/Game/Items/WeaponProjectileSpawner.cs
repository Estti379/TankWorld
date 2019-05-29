namespace TankWorld.Game.Items
{
    public class WeaponProjectileSpawner
    {
        private WeaponProjectileObject prototype;
        //Constructors
        public WeaponProjectileSpawner(WeaponProjectileObject prototype)
        {
            this.prototype = prototype;
        }

        //Accessors


        //Methods
        public WeaponProjectileObject Spawn()
        {
            return prototype.Clone();
        }
    }
}
