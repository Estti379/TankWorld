using System;
using static SDL2.SDL_ttf;
using static SDL2.SDL;
using Pong.src.ressources.Items;

namespace Pong.src.ressources.Panels
{
    public class ScorePanel : Panel
    {
        private Score score1;
        
        //Constructors
        public ScorePanel(IntPtr render)
        {
            score1 = new Score("score1", render);
        }
        
       
        

        //Accessors

        //Methods
        public override void RenderAll(IntPtr render)
        {
            score1.EntityRender(render);
        }

        public override void UpdateAll()
        {
            /*empty*/
        }
    }
}
