using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static SDL2.SDL;
using static SDL2.SDL.SDL_bool;

namespace Pong.src.ressources
{
    public class SpriteEntity
    {
        private static Dictionary<string, TextureStruct> textureList = new Dictionary<string, TextureStruct>();

        private string name;
        private SDL_Rect position;
        private SDL_Rect subDrawRect;


        //Constructors
        public SpriteEntity(string key, IntPtr render, string imagePath, byte r, byte g, byte b)
        {
            TextureStruct texture = ImageTotexture(render, imagePath, r, g, b);
            textureList.Add(key, texture);
            SetupSpriteEntity(key);
            //TODO: handle duplicate keys
        }

        public SpriteEntity(string key, TextureStruct texture)
        {
            textureList.Add(key, texture);
            SetupSpriteEntity(key);
            //TODO: handle duplicate keys or textures
        }

        public SpriteEntity(string key)
        {
            name = key;
            SetupSpriteEntity(key);
        }

        //Accessors
        public IntPtr Texture
        {
            get{ return textureList[name].texture; }
        }

        public ref SDL_Rect Pos
        {
            get { return ref position; }
        }
        public ref SDL_Rect SubRect
        {
            get { return ref subDrawRect; }
        }

        //Methods
        private void SetupSpriteEntity(string key)
        {
            
            name = key;
            position.x = 0;
            position.y = 0;
            position.w = textureList[name].w;
            position.h = textureList[name].h;

            subDrawRect.x = 0;
            subDrawRect.y = 0;
            subDrawRect.w = textureList[name].w;
            subDrawRect.h = textureList[name].h;
        }


        private TextureStruct ImageTotexture(IntPtr render, string imagePath, byte r, byte g, byte b)
        {
            TextureStruct finalTexture = new TextureStruct(IntPtr.Zero, 0, 0);
            IntPtr sprite = IntPtr.Zero;
            sprite = SDL_LoadBMP(imagePath);
            if (sprite == IntPtr.Zero)
            {
                Console.Write("Unable to load image: ball.bmp! SDL Error:" + SDL_GetError() + " \n");
                Console.Write(imagePath + " \n");
                SDL_FreeSurface(sprite);
                return finalTexture;
            }
            //Set Colorkey
            var format = ( (SDL_Surface)Marshal.PtrToStructure(sprite, typeof(SDL_Surface)) ).format;
            SDL_SetColorKey(sprite, 1, SDL_MapRGB(format, r, g, b));

            IntPtr myTexture = SDL_CreateTextureFromSurface(render, sprite);
            if (myTexture == IntPtr.Zero)
            {
                Console.Write("Unable to create texture! SDL Error:" + SDL_GetError() + " \n");
                Console.Write(imagePath + " \n");
                SDL_FreeSurface(sprite);
                return finalTexture;
            }

            finalTexture.h = ((SDL_Surface)Marshal.PtrToStructure(sprite, typeof(SDL_Surface))).h;
            finalTexture.w = ((SDL_Surface)Marshal.PtrToStructure(sprite, typeof(SDL_Surface))).w;
            finalTexture.texture = myTexture;

            SDL_FreeSurface(sprite);
            return finalTexture;
        }

        static public void RemoveSingletexture(string key)
        {
            SDL_DestroyTexture(textureList[key].texture);
            textureList.Remove(key);
        }

        public void RemoveMyTexture()
        {
            SDL_DestroyTexture(textureList[name].texture);
            textureList.Remove(name);
        }

        public void RemoveAll()
        {
            foreach(KeyValuePair<string, TextureStruct> current in textureList)
            {
                SDL_DestroyTexture(current.Value.texture);
            }

            textureList.Clear();
        }

        static public bool TextureExists(string key)
        {
            return textureList.ContainsKey(key);
        }

        public void ReplaceTexture(TextureStruct newtexture)
        {
            SDL_DestroyTexture(textureList[name].texture);
            textureList[name] = newtexture;
        }

    }
}
