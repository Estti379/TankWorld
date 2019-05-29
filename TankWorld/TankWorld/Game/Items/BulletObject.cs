using System;
using TankWorld.Engine;
using TankWorld.Game.Events;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class BulletObject :WeaponProjectileObject
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
            UpdateState(owner, startPosition, startAngle);
            model = new BulletModel(startPosition, startAngle);
        }

        //Accessors


        //Methods
        public override void Render()
        {
            model.Render();
        }

        public override void Update(ref WorldItems world)
        {
            UpdatePosition();
            model.UpdatePosition(position);
            CheckLongivity(world.player);

        }

        private void CheckLongivity(TankObject player)
        {
            if (Helper.Distance(this.position, player.Position) > MAX_RANGE )
            {
                MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.DESPAWN_PROJECTILE_ENTITY, this));
            }
        }

        private void UpdatePosition()
        {
            position.x += speedVektor.x * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
            position.y += speedVektor.y * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
        }

        public override WeaponProjectileObject Clone()
        {
            double startAngle = Math.Atan2(speedVektor.y, speedVektor.x);
            startAngle = Helper.NormalizeRad(startAngle);
            return new BulletObject(owner, position, startAngle);
        }

        internal void UpdateState(TankObject owner, Coordinate newPosition, double newtAngle)
        {
            position = newPosition;
            this.owner = owner;
            speedVektor.x = Math.Cos(newtAngle) * SPEED;
            speedVektor.y = Math.Sin(newtAngle) * SPEED;
        }
    }
}
