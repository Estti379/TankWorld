using System;
using static SDL2.SDL;

namespace TankWorld.src.ressources
{
    public struct TextureStruct
    {
        public IntPtr texture;
        public int h;
        public int w;

        public TextureStruct(IntPtr texture, int height, int width)
        {
            this.texture = texture;
            h = height;
            w = width;
        }
    }
}
