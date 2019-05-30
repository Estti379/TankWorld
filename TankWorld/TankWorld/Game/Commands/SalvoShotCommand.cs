using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Items;

namespace TankWorld.Game.Commands
{
    public class SalvoShotCommand: Command
    {
        private TankObject tank;
        private Timer time;
        private double cooldownBetweenProjectiles;
        private WeaponProjectileSpawner projectileSpawner;

        //Constructors
        public SalvoShotCommand(TankObject owner, Timer timer, WeaponProjectileSpawner projectileSpawner , double cooldownBetweenProjectiles)
        {
            tank = owner;
            time = timer;
            this.cooldownBetweenProjectiles = cooldownBetweenProjectiles;
            this.projectileSpawner = projectileSpawner;
        }

        //Accessors


        //Methods
        public override void Execute()
        {   
            LaunchBullet();
            time.ExecuteTime -= cooldownBetweenProjectiles;
            if(time.Time <= 0)
            {
                time.Pause();
            }
        }

        private void LaunchBullet()
        {
            WeaponProjectileObject bullet = projectileSpawner.Spawn();
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.SPAWN_NEW_ENTITY, bullet));
        }
    }
}
