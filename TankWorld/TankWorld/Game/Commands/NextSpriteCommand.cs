

using TankWorld.Engine;
using TankWorld.Game.Effects;

namespace TankWorld.Game.Commands
{
    public class NextSpriteCommand: Command
    {
        private BulletExplosionEffectObject effect;
        private Timer time;

        //Constructors
        public NextSpriteCommand(BulletExplosionEffectObject owner, Timer timer)
        {
            effect = owner;
            time = timer;
        }

        //Accessors


        //Methods
        public override void Execute()
        {
            effect.NextSprite();
            time.Reset();

        }

    }
}
