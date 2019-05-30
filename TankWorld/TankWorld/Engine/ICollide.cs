namespace TankWorld.Engine
{
    public interface ICollide
    {
        HitBoxStruct GetHitBoxes();
        void CheckForCollision(ICollide collidingObject);

        void HandleCollision(ICollide collidingObject, Coordinate collisionPoint);
    }
}
