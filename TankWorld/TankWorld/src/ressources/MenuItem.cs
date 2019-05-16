﻿
using System;

namespace TankWorld.src.ressources
{
    abstract public class MenuItem: IUpdate, IRender
    {
        
        //Constructors
        public MenuItem()
        {
            
        }

        //Accessors


        //Methods
        public abstract void Update();
        public abstract void Render();

        abstract public Scene Action();
        abstract public void SetPosition(int x, int y, int place);
        abstract public void ChangeStatus();
    }
}