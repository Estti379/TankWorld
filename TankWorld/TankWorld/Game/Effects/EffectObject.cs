using TankWorld.Engine;
using TankWorld.Game.Models;

namespace TankWorld.Game.Effects
{
    abstract public class EffectObject: GameObject
    {
        Timer timer;
        EffectModel model;

        //Constructors
        public EffectObject()
        {
            
        }

        public EffectModel Model { get => model; set => model = value; }
        public Timer Timer { get => timer; set => timer = value; }


        //Accessors


        //Methods
        public override void Render(RenderLayer layer)
        {
            //TODO: Cannot put Camera.IsInsideCamera, since sprite key is not know!
            if (layer == RenderLayer.OVERHEAD)
            {
                model.Render(layer);
            }
            
        }

        abstract public override void Update(ref WorldItems world);
    }
}
