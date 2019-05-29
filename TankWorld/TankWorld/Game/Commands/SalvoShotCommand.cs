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

        //Constructors
        public SalvoShotCommand(TankObject owner, Timer timer, double cooldownBetweenProjectiles)
        {
            tank = owner;
            time = timer;
            this.cooldownBetweenProjectiles = cooldownBetweenProjectiles;
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
            BulletObject bullet = new BulletObject(tank, tank.GetBarrelEndPosition(), tank.DirectionCannon);
            MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.SPAWN_BULLET_ENTITY, bullet));
        }
    }
}
