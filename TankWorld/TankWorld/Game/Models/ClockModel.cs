

using TankWorld.Engine;
using static SDL2.SDL;

namespace TankWorld.Game.Models
{
    class ClockModel: EntityModel
    {

        private int offSet;
        SDL_Color color;
        //Constructors
        public ClockModel(int offSet)
        {
            this.offSet = offSet;
            color = new SDL_Color
            {
                r = 0,
                g = 0,
                b = 0,
                a = 0
            };
            Sprite clockSprite;
            if (!Sprite.TextureExists("Clock"))
            {
                clockSprite = new Sprite("Clock", TextGenerator.pixel_millenium_medium, "NaN:NaN", color);

                clockSprite.Pos.x = offSet;
                clockSprite.Pos.y = offSet;
                AddSprite("Clock", clockSprite);
            }
        }


        //Accessors

        //Methods
        public override void Render()
        {
            AllSprites["Clock"].Render();
        }

        public void UpdateClock(string newTime)
        {
            AllSprites["Clock"].ReplaceText(newTime, TextGenerator.pixel_millenium_medium, color);
        }
    }
}
