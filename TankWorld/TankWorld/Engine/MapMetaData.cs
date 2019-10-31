using System;
using System.Collections.Generic;

namespace TankWorld.Engine
{

    public struct MapLayer
    {
        public List<int> data;
        public int height;
        public int id;
        public string name;
        public int opacity;
        public string type;
        public bool visible;
        public int width;
        public int x;
        public int y;

    }

    public struct TileSets
    {
        public int firstgid;
        public string source;
    }


    public class MapMetaData
    {

        public int height;
        public List<MapLayer> layers;
        public int nextlayerid;
        public int nextobjectid;
        public string orientation;
        public string renderorder;
        public string tiledversion;
        public int tileheight;
        public List<TileSets> tilesets;
        public int tilewidth;
        public string type;
        public double version;
        public int width;



        //Constructors
        public MapMetaData()
        {
        }

        //Accessors


        //Methods
    }
}