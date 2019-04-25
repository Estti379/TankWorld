using System;
using System.Collections.Generic;
using System.Linq;

namespace Pong.src.ressources
{
    public class Panel
    {
        private List<GameEntity> entities;
        private List<DrawnEntity> sprites;

        //Constructors
        public Panel()
        {
            entities = new List<GameEntity>();
            sprites = new List<DrawnEntity>();
        }

        //Accessors

        //Methods
        public void UpdateAll()
        {
            foreach(GameEntity entry in entities.ToList())
            {
                if (entry is IUpdateEntity toUpdate)
                {
                    toUpdate.EntityUpdate();
                }
            }
        }

        public void RenderAll(IntPtr render)
        {
            foreach (DrawnEntity entry in sprites.ToList())
            {
               entry.EntityRender(render);
            }
        }
    }
}
