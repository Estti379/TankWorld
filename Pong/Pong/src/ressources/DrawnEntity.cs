using System;
using System.Collections.Generic;

namespace Pong.src.ressources
{
    abstract public class DrawnEntity
    {
        private Dictionary<string, SpriteEntity> sprites;

        //Constructors
        public DrawnEntity()
        {
            sprites = new Dictionary<string, SpriteEntity>();
        }
        
        //Accessors
        public Dictionary<string, SpriteEntity> AllSprites
        {
            get{ return sprites; }
        }

        //Methods
        abstract public void EntityRender(IntPtr render);

        public void AddSprite(string key, SpriteEntity newSprite)
        {
            if (newSprite.Texture == IntPtr.Zero)
            {
                Console.Write("Sprite has no texture: "+ key +"\n");
            }
            else if (sprites.ContainsKey(key))
            {
                Console.Write("Key already eist in dictionnary: " + key + "\n");
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
