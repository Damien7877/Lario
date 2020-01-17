using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Map
{
    public class TileMap
    {
        private TileMapData _mapData;

        private int _numberOfTilesX, _numberOfTilesY;


        //Destination rectangle is screen position
        private Rectangle _destinationRectangle = new Rectangle();

        //source rectangle is texture position to use for draw
        private Rectangle _sourceRectangle = new Rectangle();

        public TileMap(TileMapData mapData)
        {
            //Copying object to avoid exterior modification of it
            _mapData = mapData.Clone();

            _numberOfTilesX = (_mapData.Texture.Width / _mapData.TileWidth);
            _numberOfTilesY = (_mapData.Texture.Height / _mapData.TileHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int startX = 0;
            int startY = 0;

            int endX = 15;
            int endY = 7;

            for(int y = startY; y < endY; y++)
            {
                for(int x = startX; x < endX; x++)
                {
                    _destinationRectangle.X = (x-startX) * _mapData.TileWidth;
                    _destinationRectangle.Y = (y-startY) * _mapData.TileHeight;

                    _destinationRectangle.Width = _mapData.TileWidth;
                    _destinationRectangle.Height = _mapData.TileHeight;


                    _sourceRectangle.X = (_mapData.TileMap[y, x] % _numberOfTilesX) * _mapData.TileWidth;
                    _sourceRectangle.Y =  (_mapData.TileMap[y, x] / _numberOfTilesX) * _mapData.TileHeight;

                    _sourceRectangle.Width = _mapData.TileWidth;
                    _sourceRectangle.Height = _mapData.TileHeight;

                    spriteBatch.Draw(_mapData.Texture, _destinationRectangle, _sourceRectangle, Color.White );
                }
            }
        }
    }
}
