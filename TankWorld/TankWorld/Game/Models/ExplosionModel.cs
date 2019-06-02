using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankWorld.Engine;

namespace TankWorld.Game.Models
{
    public class ExplosionModel:EffectModel
    {
        //Constructors
        public ExplosionModel(Coordinate startPosition) : base()
        {
            AddSprite("Explosion", new Sprite("Explosion", "assets/images/BulletExplosion.png", 0, 254, 0));
            this.Position = startPosition;
        }

        public override void Render(RenderLayer layer)
        {

            if (Camera.IsInsideCamera(this.Position, AllSprites["Explosion"].Pos.w, AllSprites["Explosion"].Pos.h))
            {
                Coordinate drawPosition = Camera.ConvertMapToScreenCoordinate(this.Position);
                AllSprites["Explosion"].RotateAndRender(drawPosition, 0, AllSprites["Explosion"].Pos.w / 2, AllSprites["Explosion"].Pos.h / 2);
            }
        }


        //Accessors


        //Methods
    }
}
