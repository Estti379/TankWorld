

using System;
using System.Collections.Generic;
using TankWorld.Engine;

namespace TankWorld.Game.Panels
{

    public class TiledMapPanel : MapPanel
    {
        private MapMetaData mapMetaData;
        private Dictionary<string, Sprite> tileTextures;
        private Dictionary<string, TileSetMetaData> tileSetsMetaData;
        private Coordinate tileOffset;


        //Constructor
        public TiledMapPanel(PlayScene parent, PlayParameters parameters): base(parent)
        {
            tileTextures = new Dictionary<string, Sprite>();
            tileSetsMetaData = new Dictionary<string, TileSetMetaData>();
            mapMetaData = Maploader.Instance.LoadMapMetaData(parameters.mapFileName);
            foreach (TileSets entry in mapMetaData.tilesets)
            {
                TileSetMetaData metaData = Maploader.Instance.LoadTileSetMetaData(entry.source);

                tileTextures.Add(entry.source, new Sprite(metaData.name, "assets/images/" + metaData.image, 0, 254, 0));
                tileTextures[entry.source].SubRect.h = mapMetaData.tileheight;
                tileTextures[entry.source].SubRect.w = mapMetaData.tilewidth;
                tileTextures[entry.source].Pos.h = mapMetaData.tileheight;
                tileTextures[entry.source].Pos.w = mapMetaData.tilewidth;

                tileSetsMetaData.Add(entry.source, metaData);
            }
        }

        //Accessors


        //Methods
        public override void Update()
        {
            UpdateTileOffset();
        }

        public override void Render(RenderLayer layer)
        {
            if (layer == RenderLayer.BACKGROOUND)
            {
                //Find top left and bottom right tiles that need to be rendered
                Coordinate topLeft;
                Coordinate bottomRight;
                topLeft.x = Math.Floor((CurrentCamera.Position.x - CurrentCamera.SubScreenW / 2) / mapMetaData.tilewidth);
                topLeft.y = Math.Floor((CurrentCamera.Position.y - CurrentCamera.SubScreenH / 2) / mapMetaData.tileheight);
                bottomRight.x = Math.Floor((CurrentCamera.Position.x + CurrentCamera.SubScreenW / 2) / mapMetaData.tilewidth + 1);
                bottomRight.y = Math.Floor((CurrentCamera.Position.y + CurrentCamera.SubScreenH / 2) / mapMetaData.tileheight + 1);

                string currentLayer = "GroundLayer";
                RenderAllTilesFromLayer(topLeft, bottomRight, currentLayer);

                currentLayer = "OverGroundLayer";
                RenderAllTilesFromLayer(topLeft, bottomRight, currentLayer);

            }

        }

        private void RenderAllTilesFromLayer(Coordinate topLeft, Coordinate bottomRight, string currentLayer)
        {
            int tileData;
            int tileSetIndex;
            string tileSetSource;
            Coordinate drawPosition;

            for (int y = (int)topLeft.y; y <= bottomRight.y; y++)
            {
                for (int x = (int)topLeft.x; x <= bottomRight.x; x++)
                {
                    tileData = FindDataAtPosition(x, y, currentLayer);
                    if (tileData > 0)
                    {
                        tileSetIndex = FindSourceTileSetFor(tileData);
                        tileData = tileData - mapMetaData.tilesets[tileSetIndex].firstgid + 0;
                        tileSetSource = mapMetaData.tilesets[tileSetIndex].source;

                        SetTextureTileSubRect(tileData, tileSetSource);

                        drawPosition.x = x * mapMetaData.tilewidth;
                        drawPosition.y = y * mapMetaData.tileheight;

                        drawPosition = CurrentCamera.ConvertMapToScreenCoordinate(drawPosition);

                        tileTextures[tileSetSource].RenderAtPosition((int)drawPosition.x, (int)drawPosition.y);

                    }

                }
            }
        }

        private void SetTextureTileSubRect(int tile, string source)
        {
            tileTextures[source].SubRect.x = (tile % tileSetsMetaData[source].columns) * mapMetaData.tilewidth;
            tileTextures[source].SubRect.y = (tile / tileSetsMetaData[source].columns) * mapMetaData.tilewidth;
        }

        private int FindSourceTileSetFor(int tileData)
        {


            for (int i = 0; i < mapMetaData.tilesets.Count ; i++)
            {
                if (tileData < (mapMetaData.tilesets[i].firstgid + tileSetsMetaData[mapMetaData.tilesets[i].source].tilecount))
                {
                    return i;
                }
            }


            return -1;
        }

        private int FindDataAtPosition(int x, int y, string layer)
        {
            int data = -2;
            int layerIndex = -1;

            for (int i = 0; i < mapMetaData.layers.Count; i++)
            {
                if (mapMetaData.layers[i].name == layer)
                {
                    layerIndex = i;
                }
            }

            if (layerIndex == -1)
            {
                data = -2;
                Console.WriteLine("Failled to find MapLayer " + layer +" .");
            }
            //If position is NOT inside of map boundaries
            else if( !(x >= 0 && x < mapMetaData.width && y >= 0 && y < mapMetaData.height) )
            {
                data = -1;
            }
            else
            {
                int dataIndex = x + (y * mapMetaData.width);

                data = mapMetaData.layers[layerIndex].data[dataIndex];
            }

            return data;
        }

        private void UpdateTileOffset()
        {
            tileOffset.x += -(CurrentCamera.Position.x - CurrentCamera.OldPosition.x);
            tileOffset.y += -(CurrentCamera.Position.y - CurrentCamera.OldPosition.y);

            //"While" instead of "if" just in case camera moves more than the length of a tile!
            while (tileOffset.x + mapMetaData.tilewidth < 0)
            {
                tileOffset.x += mapMetaData.tilewidth;
            }
            while (tileOffset.x > 0)
            {
                tileOffset.x -= mapMetaData.tilewidth;
            }

            while (tileOffset.y + mapMetaData.tileheight < 0)
            {
                tileOffset.y += mapMetaData.tileheight;
            }
            while (tileOffset.y > 0)
            {
                tileOffset.y -= mapMetaData.tileheight;
            }
        }

    }
}
