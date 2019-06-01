using System;
using System.Collections.Generic;
using TankWorld.Engine;
using TankWorld.Game.Commands;
using TankWorld.Game.Components;
using TankWorld.Game.Effects;
using TankWorld.Game.Events;
using TankWorld.Game.Models;

namespace TankWorld.Game.Items
{
    public class BulletObject : WeaponProjectileObject
    {
        private BulletModel model;
        private TankObject owner;

        private const byte SPEED = 30;
        private const double MAX_TRAVEL_TIME = 2000; //in milliseconds

        private PhysicsComponent bulletPhysics;

        private Coordinate speedVektor;

        private Timer longivity;

        //Constructors
        public BulletObject(TankObject owner, Coordinate startPosition, double startAngle) : base()
        {
            UpdateState(owner, startPosition, startAngle);
            model = new BulletModel(startPosition, startAngle);
            longivity = new Timer(Timer.Type.DESCENDING)
            {
                Time = MAX_TRAVEL_TIME,
                Command = new EraseGameObjectCommand(this)
            };
            bulletPhysics = new BulletPhysicsComponent(this);
        }

        //Accessors
        public BulletModel Model { get => model;}
        public Coordinate SpeedVektor { get => speedVektor;}

        //Methods
        public override void Render()
        {
            model.Render();
        }

        public override void Update(ref WorldItems world)
        {
            bulletPhysics.Update(this, ref world);
            UpdatePosition();
            model.UpdatePosition(Position);
            longivity.Update();

        }

        private void UpdatePosition()
        {
            Position.x += speedVektor.x * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
            Position.y += speedVektor.y * GameConstants.MS_PER_UPDATE * 1 / 1000 * GameConstants.METER_TO_PIXEL;
        }

        public override WeaponProjectileObject Clone()
        {
            double startAngle = Math.Atan2(speedVektor.y, speedVektor.x);
            startAngle = Helper.NormalizeRad(startAngle);
            return new BulletObject(owner, Position, startAngle);
        }

        internal void UpdateState(TankObject owner, Coordinate newPosition, double newtAngle)
        {
            Position = newPosition;
            this.owner = owner;
            speedVektor.x = Math.Cos(newtAngle) * SPEED;
            speedVektor.y = Math.Sin(newtAngle) * SPEED;
        }

        public override HitBoxStruct GetHitBoxes()
        {
            return this.bulletPhysics.HitBoxes;
        }

        public override void CheckForCollision(ICollide collidingObject)
        {
            Coordinate collisionPoint = new Coordinate();
            foreach (KeyValuePair<string, HitBox> myBox in this.GetHitBoxes().hitBoxesList)
            {
                foreach (KeyValuePair<string, HitBox> otherBox in collidingObject.GetHitBoxes().hitBoxesList)
                {
                    if (Helper.HitBoxIntersection(myBox.Value, otherBox.Value, ref collisionPoint))
                    {
                        this.HandleCollision(collidingObject, collisionPoint);
                        break; //Stop right after first point found!
                    }
                }
            }
        }

        public override void HandleCollision(ICollide collidingObject, Coordinate collisionPoint)
        {
            TankObject collidingTank = collidingObject as TankObject;
            if (collidingTank != null)
            {
                if(this.owner.Id != collidingTank.Id)
                {
                    MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.DESPAWN_ENTITY, this));
                    GameObject newExplosion = new BulletExplosionEffectObject(collisionPoint);
                    MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.SPAWN_NEW_ENTITY, newExplosion));
                    MainEventBus.PostEvent(new SceneStateEvent(SceneStateEvent.Type.TANK_HIT, collidingTank, this.owner));

                }
            }
        }

        public override void RenderHitBoxes()
        {
            throw new NotImplementedException();
        }
    }
}
