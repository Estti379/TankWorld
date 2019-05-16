using System.Collections.Generic;

namespace TankWorld.src.ressources.Models
{
    public class TankModel : EntityModel
    {
        private string ID;

        private double xCoordinates;
        private double yCoordinates;
        private double directionAngle;
        //Constructors
        public TankModel(string ID)
        {
            this.ID = ID;
            AddSprite("TankBody", new Sprite(ID+"TankBody", "assets/images/TankBody.bmp", 0, 254, 0) );
        }

        //Accessors


        //Methods

        public override void Render()
        {
            AllSprites["TankBody"].RotateAndRender(xCoordinates, yCoordinates, directionAngle, AllSprites["TankBody"].SubRect.w/2, AllSprites["TankBody"].SubRect.h/2);
        }
        public void UpdateModel(double x, double y, double direction)
        {
            xCoordinates = x;
            yCoordinates = y;
            directionAngle = direction;
        }
    }
}
