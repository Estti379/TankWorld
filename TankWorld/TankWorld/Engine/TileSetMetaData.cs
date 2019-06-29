using System;
using System.Collections.Generic;

namespace TankWorld.Engine
{

    public struct Tile
    {
        public int id;
        public List<TileProperties> properties;
    }

    public struct TileProperties
    {
        public string name;
        public string type;
        //carefull: reference type of value is dynamic!!
        //always check runtime type of variable using "string type" above
        public dynamic value;
    }


    public class TileSetMetaData
    {

        public int columns;
        public string image;
        public int imageheight;
        public int imagewidth;
        public int margin;
        public string name;
        public int spacing;
        public int tilecount;
        public string tiledversion;
        public int tileheight;
        public List<Tile> tiles;
        public int tilewidth;
        public string type;
        public double version;





        //Constructors
        public TileSetMetaData()
        {
        }

        //Accessors


        //Methods
    }
}