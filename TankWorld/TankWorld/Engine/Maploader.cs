/*
            //Debugging
            string metaString2 = JsonConvert.SerializeObject(metaData, Formatting.Indented);
            Console.WriteLine(metaString);
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine(metaString2);
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            File.WriteAllText(mapDirectory+"debug_copy_" + path, metaString2);
            //Debugging end
*/




using Newtonsoft.Json;
using System;
using System.IO;




namespace TankWorld.Engine
{
    public class Maploader
    {
        static private Maploader singleton = null;

        static readonly string mapDirectory = "assets/maps/";
        static readonly string tileSetDirectory = "assets/maps/tileSet/";
        //Constructors
        private Maploader()
        {

        }

        public static Maploader Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Maploader();
                }
                return singleton;
            }
        }

        //Accessors


        //Methods
        public MapMetaData LoadMapMetaData(string path)
        {

            string metaString = File.ReadAllText(mapDirectory+path);
            MapMetaData metaData = JsonConvert.DeserializeObject<MapMetaData>(metaString);

            //Debugging
            //string metaString2 = JsonConvert.SerializeObject(metaData, Formatting.Indented);
            //File.WriteAllText(mapDirectory+"debug_copy_" + path, metaString2);
            //Debugging end


            return metaData;
        }
        public TileSetMetaData LoadTileSetMetaData(string path)
        {

            string metaString = File.ReadAllText(tileSetDirectory + path);
            TileSetMetaData metaData = JsonConvert.DeserializeObject<TileSetMetaData>(metaString);

            //Debugging
            //string metaString2 = JsonConvert.SerializeObject(metaData, Formatting.Indented);
            //File.WriteAllText(tileSetDirectory + "debug_copy_" + path, metaString2);
            //Debugging end


            return metaData;
        }

    }
}
