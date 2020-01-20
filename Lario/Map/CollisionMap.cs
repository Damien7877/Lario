using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Map
{
    public class CollisionMap
    {
        private TileMapData _mapData;

        private int _numberOfTilesX;


        //Destination rectangle is screen position
        private Rectangle _destinationRectangle = new Rectangle();

        //source rectangle is texture position to use for draw
        private Rectangle _sourceRectangle = new Rectangle();

        public CollisionMap(TileMapData mapData)
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
                for (int x = startX; x < endX; x++)
                {
                    //set the position on screen
                    SetDestinationRectangle(y, x);

                    //set the position in the texture
                    SetSourceRectangle(_mapData.TileMap[y, x]);

                    spriteBatch.Draw(_mapData.Texture, _destinationRectangle, _sourceRectangle, Color.White);
                }
            }
        }

        public bool IsOutOfBound(Vector2 position)
        {
            if(position.X < 0)
            {
                return true;
            }

            if(position.X > _mapData.MapWidth * _mapData.TileWidth)
            {
                return true;
            }

            if (position.Y > _mapData.MapHeight * _mapData.TileHeight)
            {
                return true;
            }

            return false;
        }

        public bool IsCollisionRight(Rectangle boxTested)
        {
            return  IsCollision(boxTested, -2, 2, -1, 1); 
        }

        public bool IsCollisionLeft(Rectangle boxTested)
        {
            
            return IsCollision(boxTested, -2, 2, -1, 1);
        }

        public bool IsCollisionDown(Rectangle boxTested)
        {
            return IsCollision(boxTested, 0, 1, 1, 2);
        }

        public bool IsCollisionUp(Rectangle boxTested)
        {
            return IsCollision(boxTested, 0, 1, -1, 0);
        }

        public bool IsCollision(Rectangle boxTested, int startOffsetX, int endOffsetX, int startOffsetY, int endOffsetY)
        {
            Vector2 testedOrigin = GetTestedOriginOnMap(boxTested);

            int startX = Math.Max((int)testedOrigin.X + startOffsetX, 0);
            int endX = Math.Min((int)testedOrigin.X + endOffsetX, _mapData.MapWidth);

            int startY = Math.Max((int)testedOrigin.Y + startOffsetY, 0);
            int endY = Math.Min((int)testedOrigin.Y + endOffsetY, _mapData.MapHeight);


            for (int y = startY; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    Rectangle collisionBoxTotest = new Rectangle(
                                            x * _mapData.TileWidth,
                                            y * _mapData.TileHeight,
                                            _mapData.TileWidth,
                                            _mapData.TileHeight
                                        );

                    if (_mapData.TileMap[y, x] == 1 
                        && boxTested.Intersects(collisionBoxTotest))
                    {
                        return true;
                    }
                }
            }

            return false;
        }




        private Vector2 GetTestedOriginOnMap(Rectangle boxTested)
        {
            decimal posX = (decimal)boxTested.X + (boxTested.Width / 2);
            decimal posY = (decimal)boxTested.Y + (boxTested.Height -10);
            int positionXMap = (int)Math.Floor(posX / (decimal)_mapData.TileWidth);
            int positionYMap = (int)Math.Floor(posY / (decimal) _mapData.TileHeight);

            return new Vector2(positionXMap, positionYMap);
        }

        private void SetSourceRectangle(int tileNumber)
        {
            _sourceRectangle.X = (tileNumber % _numberOfTilesX) * _mapData.TileWidth;
            _sourceRectangle.Y = (tileNumber / _numberOfTilesX) * _mapData.TileHeight;

        }

        private void SetDestinationRectangle( int y, int x)
        {
            _destinationRectangle.X = (x * _mapData.TileWidth) ;
            _destinationRectangle.Y = (y * _mapData.TileHeight);

        }


    }
}
