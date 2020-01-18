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
        private const int EmptyCell = -1;
        private TileMapData _mapData;

        private int _numberOfTilesX;


        //Destination rectangle is screen position
        private Rectangle _destinationRectangle = new Rectangle();

        //source rectangle is texture position to use for draw
        private Rectangle _sourceRectangle = new Rectangle();

        public TileMap(TileMapData mapData)
        {
            //Copying object to avoid exterior modification of it
            _mapData = mapData.Clone();

            _numberOfTilesX = (_mapData.Texture.Width / _mapData.TileWidth);
        }

        public void Draw(SpriteBatch spriteBatch, Camera.Camera camera)
        {
            Rectangle viewPort = camera.CurrentViewPort;

            int startX = Math.Max(viewPort.X / _mapData.TileWidth , 0);
            int startY = Math.Max(viewPort.Y / _mapData.TileHeight, 0);

            int endX = Math.Min((viewPort.Right  / _mapData.TileWidth) + 1, _mapData.MapWidth);
            int endY = Math.Min((viewPort.Bottom / _mapData.TileHeight) + 1, _mapData.MapHeight);

            for(int y = startY; y < endY; y++)
            {
                for(int x = startX; x < endX; x++)
                {
                    if(_mapData.TileMap[y, x] > EmptyCell)
                    {
                        _destinationRectangle.X =  (x  * _mapData.TileWidth) - viewPort.X;
                        _destinationRectangle.Y = (y * _mapData.TileHeight) - viewPort.Y;

                        _destinationRectangle.Width = _mapData.TileWidth;
                        _destinationRectangle.Height = _mapData.TileHeight;


                        _sourceRectangle.X = (_mapData.TileMap[y, x] % _numberOfTilesX) * _mapData.TileWidth;
                        _sourceRectangle.Y = (_mapData.TileMap[y, x] / _numberOfTilesX) * _mapData.TileHeight;

                        _sourceRectangle.Width = _mapData.TileWidth;
                        _sourceRectangle.Height = _mapData.TileHeight;

                        spriteBatch.Draw(_mapData.Texture, _destinationRectangle, _sourceRectangle, Color.White);
                    }
                }
            }
        }
    }
}
