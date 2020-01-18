using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Map
{
    public class Map
    {
        private List<TileMap> _layers = new List<TileMap>();

        private CollisionMap _collisionMap;

        private Camera.Camera _camera;

        public Map(Camera.Camera camera)
        {
            _camera = camera;
        }

        public bool IsDrawCollisionMap { get; set; }

        public void SetCollisionMap(CollisionMap collisionMap)
        {
            _collisionMap = collisionMap;
        }

        public bool IsCollisionUp(Rectangle boxTested)
        {
            return _collisionMap.IsCollisionUp(boxTested);
        }

        public bool IsCollisionDown(Rectangle boxTested)
        {
            return _collisionMap.IsCollisionDown(boxTested);
        }

        public bool IsCollisionLeft(Rectangle boxTested)
        {
            return _collisionMap.IsCollisionLeft(boxTested);
        }

        public bool IsCollisionRight(Rectangle boxTested)
        {
            return _collisionMap.IsCollisionRight(boxTested);
        }


        public void AddLayer(TileMap layer)
        {
            _layers.Add(layer);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var layer in _layers)
            {
                layer.Draw(spriteBatch, _camera);
            }

            if(IsDrawCollisionMap)
            {
                _collisionMap.Draw(spriteBatch, _camera);
            }

        }
    }
}
