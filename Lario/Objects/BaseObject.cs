using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Objects
{
    public abstract class BaseObject
    {
        public Vector2 Position { get; set;  }

        protected Texture2D _texture;

        protected BaseObject(Texture2D texture)
        {
            _texture = texture;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
