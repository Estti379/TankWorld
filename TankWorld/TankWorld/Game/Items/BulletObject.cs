using System;
using TankWorld.Engine;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class BulletObject : IRender, IUpdate
    {
        private BulletModel model;
        private TankObject owner;
        private const byte SPEED = 30;

        private Coordinate position;
        private Coordinate speedVektor;

        //Constructors
        public BulletObject(TankObject owner, Coordinate startPosition, double startAngle)
        {
            position = startPosition;
            this.owner = owner;
            model = new BulletModel(startPosition, startAngle);
            speedVektor.x = Math.Cos(startAngle) * SPEED;
            speedVektor.y = Math.Sin(startAngle) * SPEED;
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
            position.x += speedVektor.x * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
            position.y += speedVektor.y * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
        }
    }
}
