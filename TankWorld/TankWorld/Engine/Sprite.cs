using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL.SDL_bool;

namespace TankWorld.Engine
{
    public class Sprite
    {
        private static Dictionary<string, TextureStruct> textureList = new Dictionary<string, TextureStruct>();
        private static IntPtr renderer = IntPtr.Zero;

        private string name;
        private SDL_Rect position;
        private SDL_Rect subDrawRect;


        //Constructors
        public Sprite(string key, string imagePath, byte r, byte g, byte b)
        {
            TextureStruct texture;
            if (textureList.ContainsKey(key))
            {
                texture = textureList[key];
            }
            else
            {
                texture = BMPToTexture(imagePath, r, g, b);
                textureList.Add(key, texture);
            }
            SetupSpriteEntity(key);
        }
        public Sprite(string key, IntPtr font, string text, SDL_Color color)
        {
            TextureStruct texture;
            if (textureList.ContainsKey(key))
            {
                texture = textureList[key];
            }
            else
            {
                texture = TextToTexture(font, text, color);
                textureList.Add(key, texture);
            }
            SetupSpriteEntity(key);
        }

        public void ReplaceText(string newText, IntPtr font, SDL_Color color)
        {
            TextureStruct newTexture = TextToTexture(font, newText, color);
            this.ReplaceTexture(newTexture);
        }

        public Sprite(string key, TextureStruct texture)
        {
            if (textureList.ContainsKey(key))
            {
                texture = textureList[key];
            }
            SetupSpriteEntity(key);
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

        public IntPtr Texture
        {
            get { return textureList[name].texture; }
        }
        public int TextureHeight
        {
            get { return textureList[name].h; }
        }
        public int TextureWidth
        {
            get { return textureList[name].w; }
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


        public TextureStruct TextToTexture(IntPtr font, string text, SDL_Color color)
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

        private TextureStruct BMPToTexture(string imagePath, byte r, byte g, byte b)
        {
            TextureStruct finalTexture = new TextureStruct(IntPtr.Zero, 0, 0);
            IntPtr sprite = IntPtr.Zero;

            //loads all (supported) images, not only BMP
            sprite = SDL_image.IMG_Load(imagePath);
            //sprite = SDL_LoadBMP(imagePath);
            if (sprite == IntPtr.Zero)
            {
                Console.Write("Unable to load image: " + imagePath + " SDL Error:" + SDL_GetError() + " \n");
                SDL_FreeSurface(sprite);
                return finalTexture;
            }
            //Set Colorkey
            var format = ((SDL_Surface)Marshal.PtrToStructure(sprite, typeof(SDL_Surface))).format;
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
            foreach (KeyValuePair<string, TextureStruct> current in textureList)
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

        public void RenderAtPosition(int x, int y)
        {
            Pos.x = x;
            Pos.y = y;

            SDL_RenderCopy(renderer, textureList[name].texture,
                       ref subDrawRect,
                       ref position);
        }
        public void RotateAndRender(Coordinate screenPosition, double angleRad, double originX, double originY)
        {
            SDL_Point centerOfRotation = new SDL_Point()
            {
                x = (int)Math.Round(originX),
                y = (int)Math.Round(originY)
            };

            position.x = (int)Math.Round(screenPosition.x - subDrawRect.w / 2);
            position.y = (int)Math.Round(screenPosition.y - subDrawRect.h / 2);

            double angleDeg = angleRad * 180 / Math.PI;

            SDL_RenderCopyEx(renderer, textureList[name].texture,
                       ref subDrawRect,
                       ref position,
                       angleDeg,
                       ref centerOfRotation,
                       SDL_RendererFlip.SDL_FLIP_NONE);
        }

        //TODO: change it to draw into a new texture instead of on screen! (SDL_SetRenderTarget)
        static public void DrawFilledRectangle(Coordinate A, Coordinate B, Coordinate C, Coordinate D, SDL_Color color)
        {
            SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
            Coordinate low = new Coordinate();
            Coordinate left = new Coordinate();
            Coordinate high = new Coordinate();
            Coordinate right = new Coordinate();

            //Case A is the lowest point in X-Axis 
            if ((A.x <= D.x) && (A.x <= B.x))
            {
                low = A;
                high = C;
                left = B;
                right = D;

            }
            //Case B is the lowest point in X-Axis 
            else if ((B.x <= C.x) && (B.x <= A.x))
            {
                low = B;
                high = D;
                left = C;
                right = A;

            }
            //Case C is the lowest point in X-Axis 
            else if ((C.x <= D.x) && (C.x <= B.x))
            {
                low = C;
                high = A;
                left = D;
                right = B;


            }
            //Case D is the lowest point in X-Axis 
            else if ((D.x <= C.x) && (D.x <= A.x))
            {
                low = D;
                high = B;
                left = A;
                right = C;

            }


            SDL_Rect line;
            //Handle cases of rectangles that are NOT angled
            if (low.x - left.x == 0)
            {//it is highly possible that one needs to use right instead of left here because of X-axe inversion!
                line.x = (int)Math.Round(low.x);
                line.y = (int)Math.Round(low.y);
                line.h = (int)Math.Round(left.y - low.y);
                line.w = (int)Math.Round(right.x - low.x);

                //draw Rectangle
                SDL_RenderFillRect(renderer, ref line);
                return;
                //Leave because everything is draw!
            }


            //Get line equations for all 4 lines; y= m*x + p
            double mLowLeft = (low.y - left.y) / (double)(low.x - left.x);
            double pLowLeft = low.y - mLowLeft * low.x;

            double mLowRight = (low.y - right.y) / (double)(low.x - right.x);
            double pLowRight = low.y - mLowRight * low.x;

            double mLeftHigh = (high.y - left.y) / (double)(high.x - left.x);
            double pLeftHigh = high.y - mLeftHigh * high.x;

            double mRightHigh = (high.y - right.y) / (double)(high.x - right.x);
            double pRightHigh = high.y - mRightHigh * high.x;

            double mSmall = mLowLeft;
            double pSmall = pLowLeft;
            double mBig = mLowRight;
            double pBig = pLowRight;

            //Draw rectangle low1, low2, high1, high2 starting from left to right

            for (double x = low.x; x < high.x; x++)
            {
                if (x >= left.x)
                {
                    mSmall = mLeftHigh;
                    pSmall = pLeftHigh;
                }
                if (x >= right.x)
                {
                    mBig = mRightHigh;
                    pBig = pRightHigh;
                }

                line.x = (int)Math.Round(x);
                line.y = (int)Math.Round(x * mBig + pBig);
                line.h = (int)Math.Round(-x * mBig - pBig + x * mSmall + pSmall);
                line.w = 1;

                //draw Rectangle
                SDL_RenderFillRect(renderer, ref line);

            }

        }

        //TODO: change it to draw into a new texture instead of on screen! (SDL_SetRenderTarget)
        static public void DrawFilledCircle(Coordinate center, double radius, SDL_Color color)
        {
            SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
            SDL_Rect line;
            double xUp;
            double yUp;

            for (double x = 0; x < 2*radius; x++)
            {
                xUp = x - radius;
                yUp = Math.Sqrt(radius * radius - xUp * xUp);

                line.x = (int)Math.Round(xUp+center.x);
                line.y = (int)Math.Round(-yUp+center.y);
                line.w = 1;
                line.h = (int)Math.Round(2*yUp);
                //draw Rectangle
                SDL_RenderFillRect(renderer, ref line);
            }
        }
    }
}
