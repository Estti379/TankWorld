using System;
using System.Collections.Generic;
using System.Linq;

namespace TankWorld.src.ressources
{
    abstract public class Panel: IRender, IUpdate
    {

        //Constructors
        public Panel()
        {

        }


        //Accessors


        //Methods
        public abstract void Update();
        public abstract void Render();
    }
}
