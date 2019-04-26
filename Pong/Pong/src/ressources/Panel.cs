using Pong.src.ressources.Panels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pong.src.ressources
{
    abstract public class Panel
    {

        //Constructors
        public Panel()
        {

        }

        //Accessors


        //Methods
        abstract public void RenderAll(IntPtr render);

        abstract public void UpdateAll(Panel panel);
    }
}
