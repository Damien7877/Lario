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

        private TileMap _collisionMap;

        private Camera.Camera _camera;

        public Map(Camera.Camera camera)
        {
            _camera = camera;
        }

        public bool IsDrawCollisionMap { get; set; }

        public void SetCollisionMap(TileMap collisionMap)
        {
            _collisionMap = collisionMap;
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
