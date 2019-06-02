using System;
using System.Collections.Generic;

namespace TankWorld.Engine
{
    abstract public class EntityModel: IRender
    {
        private Dictionary<string, Sprite> sprites;

        //Constructors
        public EntityModel()
        {
            sprites = new Dictionary<string, Sprite>();
        }
        
        //Accessors
        public Dictionary<string, Sprite> AllSprites
        {
            get{ return sprites; }
        }

        //Methods
        abstract public void Render();

        public void AddSprite(string key, Sprite newSprite)
        {
            if (newSprite.Texture == IntPtr.Zero)
            {
                Console.Write("Sprite has no texture: "+ key +"\n");
            }
            else if (sprites.ContainsKey(key))
            {
                Console.Write("Key already exist in dictionnary: " + key + "\n");
            }
            else
            {
                sprites.Add(key, newSprite);
            }
        }

        public void RemoveSprite(string key)
        {
                sprites.Remove(key);
        }

    }
}
