using System;
using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class BulletObject : IRender
    {
        private BulletModel model;
        private TankObject owner;
        private const byte SPEED = 30;
        private const int MAX_RANGE = 10000;

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

        public void Update(ref WorldItems world)
        {
            UpdatePosition();
            model.UpdatePosition(position);
            CheckLongivity(world.player);

        }

        private void CheckLongivity(TankObject player)
        {
            if (Helper.Distance(this.position, player.Position) > MAX_RANGE )
            {
                MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.DESPAWN_BULLET_ENTITY, this));
            }
        }

        private void UpdatePosition()
        {
            position.x += speedVektor.x * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
            position.y += speedVektor.y * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
        }


    }
}
