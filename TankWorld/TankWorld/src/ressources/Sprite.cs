using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL.SDL_bool;

namespace TankWorld.src.ressources
{
    public class Sprite: IRender
    {
        private static Dictionary<string, TextureStruct> textureList = new Dictionary<string, TextureStruct>();
        private static IntPtr renderer = IntPtr.Zero;

        private string name;
        private SDL_Rect position;
        private SDL_Rect subDrawRect;


        //Constructors
        public Sprite(string key, string imagePath, byte r, byte g, byte b)
        {
            TextureStruct texture = ImageToTexture(imagePath, r, g, b);
            textureList.Add(key, texture);
            SetupSpriteEntity(key);
            //TODO: handle duplicate keys
        }
        public Sprite(string key, IntPtr font, string text, SDL_Color color)
        {
            TextureStruct texture = TextToTexture(font, text, color);
            textureList.Add(key, texture);
            SetupSpriteEntity(key);
        }

        public Sprite(string key, TextureStruct texture)
        {
            textureList.Add(key, texture);
            SetupSpriteEntity(key);
            //TODO: handle duplicate keys or textures
        }

        public Sprite(string key)
        {
            name = key;
            SetupSpriteEntity(key);
        }

        //Accessors
        static public IntPtr Renderer
        {
            set { renderer = value; }
        }
        //TODO: Is this still needed?
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


        private TextureStruct TextToTexture(IntPtr font, string text, SDL_Color color)
        {
            TextureStruct finalTexture = new TextureStruct(IntPtr.Zero, 0, 0);
            IntPtr textSurface = TTF_RenderText_Solid(font, text, color);
            IntPtr myTexture = IntPtr.Zero;
            if (textSurface == IntPtr.Zero)
            {
                Console.Write("Unable to render text surface! SDL_ttf Error: %s\n", SDL_GetError());
            }
            else
            {
                //Create texture from surface pixels
                myTexture = SDL_CreateTextureFromSurface(renderer, textSurface);
                if (myTexture == IntPtr.Zero)
                {
                    Console.Write("Unable to create texture from rendered text! SDL Error: %s\n", SDL_GetError());
                }
                //Create texture struct
                finalTexture.h = ((SDL_Surface)Marshal.PtrToStructure(textSurface, typeof(SDL_Surface))).h;
                finalTexture.w = ((SDL_Surface)Marshal.PtrToStructure(textSurface, typeof(SDL_Surface))).w;
                finalTexture.texture = myTexture;
                //Get rid of old surface
                SDL_FreeSurface(textSurface);
            }
            return finalTexture;
        }

        private TextureStruct ImageToTexture(string imagePath, byte r, byte g, byte b)
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

            IntPtr myTexture = SDL_CreateTextureFromSurface(renderer, sprite);
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

        static public void RemoveAll()
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

        public void Render()
        {
            SDL_RenderCopy(renderer, textureList[name].texture,
                       ref subDrawRect,
                       ref position);
        }
        public void RotateAndRender(Coordinate mapPosition, double angleRad, double originX, double originY)
        {
            SDL_Point centerOfRotation = new SDL_Point()
            {
                x = (int)Math.Round(originX),
                y = (int)Math.Round(originY)
            };
            
            position.x = (int) Math.Round(mapPosition.x - subDrawRect.w / 2);
            position.y = (int) Math.Round(mapPosition.y - subDrawRect.h / 2);

            double angleDeg = angleRad * 180 / Math.PI;

            SDL_RenderCopyEx(renderer, textureList[name].texture,
                       ref subDrawRect,
                       ref position,
                       angleDeg,
                       ref centerOfRotation,
                       SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
