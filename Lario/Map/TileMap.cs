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

            _sourceRectangle.Width = _mapData.TileWidth;
            _sourceRectangle.Height = _mapData.TileHeight;

            _destinationRectangle.Width = _mapData.TileWidth;
            _destinationRectangle.Height = _mapData.TileHeight;
        }

        public void Draw(SpriteBatch spriteBatch, Camera.Camera camera)
        {
            var screenRectangle = camera.ViewportWorldBoundry();

            int startX = (int)Math.Max(0, screenRectangle.X / _mapData.TileWidth - 1);
            int startY = (int)Math.Max(0, screenRectangle.Y / _mapData.TileHeight - 1);

            int endX = Math.Min(_mapData.MapWidth, screenRectangle.Right / _mapData.TileWidth + 1);
            int endY = Math.Min(_mapData.MapHeight, screenRectangle.Bottom / _mapData.TileHeight + 1);

            for (int y = startY; y < endY; y++)
            {
                for(int x = startX; x < endX; x++)
                {
                    if(_mapData.TileMap[y, x] > EmptyCell)
                    {
                        SetDestinationRectangle(y, x);

                        SetSourceRectangle(_mapData.TileMap[y, x]);

                        spriteBatch.Draw(_mapData.Texture, _destinationRectangle, _sourceRectangle, Color.White);
                    }
                }
            }
        }

        private void SetSourceRectangle(int tileNumber)
        {
            _sourceRectangle.X = (tileNumber % _numberOfTilesX) * _mapData.TileWidth;
            _sourceRectangle.Y = (tileNumber / _numberOfTilesX) * _mapData.TileHeight;


        }

        private void SetDestinationRectangle(int y, int x)
        {
            _destinationRectangle.X = (x * _mapData.TileWidth) ;
            _destinationRectangle.Y = (y * _mapData.TileHeight);


        }
    }
}
