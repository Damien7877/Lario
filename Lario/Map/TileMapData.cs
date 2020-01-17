using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Map
{
    public class TileMapData
    {
        public int[,] TileMap { get; set; }

        public int TileWidth { get; set; }

        public int TileHeight { get; set; }

        public int MapHeight { get; set; }

        public int MapWidth { get; set; }

        public Texture2D Texture { get; set; }

        public TileMapData Clone()
        {
            return new TileMapData()
            {
                MapHeight = MapHeight,
                MapWidth = MapWidth,
                Texture = Texture,
                TileHeight = TileHeight,
                TileMap = TileMap,
                TileWidth = TileWidth
            };
        }
    }
}
