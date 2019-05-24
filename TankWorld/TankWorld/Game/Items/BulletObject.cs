using System;
using TankWorld.Engine;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class BulletObject : IRender, IUpdate
    {
        private BulletModel model;
        private TankObject owner;
        private const byte speed = 1;

        private Coordinate position;
        private Coordinate speedVektor;

        //Constructors
        public BulletObject(TankObject owner, Coordinate startPosition, double startAngle)
        {
            position = startPosition;
            this.owner = owner;
            model = new BulletModel(startPosition, startAngle);
            speedVektor.x = Math.Cos(startAngle) * speed;
            speedVektor.y = Math.Sin(startAngle) * speed;
        }

        //Accessors


        //Methods
        public void Render()
        {
            model.Render();
        }

        public void Update()
        {
            UpdatePosition();
            model.UpdatePosition(position);
        }
        private void UpdatePosition()
        {
            position.x += speedVektor.x;
            position.y += speedVektor.y;
        }
    }
}
