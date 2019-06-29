
using Newtonsoft.Json;
using System;
using System.IO;

namespace TankWorld.Engine
{
    public class Maploader
    {
        static readonly string mapDirectory = "assets/maps/";
        //Constructors
        public Maploader()
        {
        }

        //Accessors


        //Methods
        public MapMetaData LoadMapMetaData(string path)
        {

            string metaString = File.ReadAllText(mapDirectory+path);
            MapMetaData metaData = JsonConvert.DeserializeObject<MapMetaData>(metaString);

            //Debugging
            string metaString2 = JsonConvert.SerializeObject(metaData, Formatting.Indented);
            Console.WriteLine(metaString);
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine(metaString2);
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            File.WriteAllText(mapDirectory+"debug_copy.json", metaString2);
            //Debugging end


            return metaData;
        } 
    }
}
