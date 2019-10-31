using System;
using TankWorld.Engine;
using TankWorld.Game.Components;

namespace TankWorld.Game.Panels
{
    abstract public class MapPanel : Panel
    {

        private PlayScene parent;

        //Constructors
        public MapPanel(PlayScene parent)
        {
            this.parent = parent;
        }

   

        //Accessors
        public Camera CurrentCamera { get => parent.CurrentCamera; }

        //Methods

        
    }
}
