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

        public override void Render()
        {
            Coordinate drawPosition = Camera.ConvertMapToScreenCoordinate(this.Position);
            bool isInsideCamera = false;

            isInsideCamera = (drawPosition.x >= 0 - AllSprites["Explosion"].Pos.w)
                && (drawPosition.x <= GameConstants.WINDOWS_X + AllSprites["Explosion"].Pos.w)
                && (drawPosition.y >= 0 - AllSprites["Explosion"].Pos.h)
                && (drawPosition.y <= GameConstants.WINDOWS_Y + AllSprites["Explosion"].Pos.h);

            if (isInsideCamera)
            {
                AllSprites["Explosion"].RotateAndRender(drawPosition, 0, AllSprites["Explosion"].Pos.w / 2, AllSprites["Explosion"].Pos.h / 2);
            }
        }


        //Accessors


        //Methods
    }
}
