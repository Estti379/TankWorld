using TankWorld.Engine;
using TankWorld.Game.Commands;
using TankWorld.Game.Events;
using TankWorld.Game.Models;

namespace TankWorld.Game.Effects
{
    public class BulletExplosionEffectObject: EffectObject
    {
        private const int NB_SPRITES = 4;
        private const double TIME_PER_SPRITE = 2*16;//milliseconds
        private int currentSprite;

        
        //Constructors
        public BulletExplosionEffectObject(Coordinate startPosition)
        {
            this.currentSprite = 0;
            this.Model = new ExplosionModel(startPosition);
            this.Model.AllSprites["Explosion"].SubRect.w = Model.AllSprites["Explosion"].TextureWidth / NB_SPRITES;
            this.Model.AllSprites["Explosion"].Pos.w = Model.AllSprites["Explosion"].TextureWidth / NB_SPRITES;
            this.Model.AllSprites["Explosion"].Pos.w /= 1;
            this.Model.AllSprites["Explosion"].Pos.h /= 1;

            this.Timer = new Timer(Timer.Type.DESCENDING);
            this.Timer.Time = TIME_PER_SPRITE;
            this.Timer.DefaultTime = TIME_PER_SPRITE;
            this.Timer.ExecuteTime = 0;
            this.Timer.Command = new NextSpriteCommand(this, Timer);
        }


        //Accessors


        //Methods

        public override void Update(ref WorldItems world)
        {
            this.Timer.Update();
        }

        public void NextSprite()
        {
            this.Model.AllSprites["Explosion"].SubRect.x = currentSprite * (Model.AllSprites["Explosion"].TextureWidth / NB_SPRITES);

            currentSprite++;
            if(currentSprite > NB_SPRITES)
            {
                MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.DESPAWN_ENTITY, this) );
            }
        }
    }
}
