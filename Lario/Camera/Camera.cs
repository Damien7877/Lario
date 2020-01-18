using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Camera
{
    public class Camera
    {
        private Vector2 _currentPosition = Vector2.Zero;

        private Vector2 _viewportSize = Vector2.Zero;

        private Rectangle _worldSize;


        public Rectangle CurrentViewPort
        { 
            get 
            {
                return new Rectangle((int)_currentPosition.X,
                                     (int)_currentPosition.Y,
                                     (int)_viewportSize.X,
                                     (int)_viewportSize.Y);
            }
        }

        public Camera(Rectangle worldSize, Vector2 viewportSize)
        {
            _worldSize = worldSize;

            _viewportSize = viewportSize;
        }

        public void Move(Vector2 offset)
        {
            
            _currentPosition += offset;

        }
    }
}
