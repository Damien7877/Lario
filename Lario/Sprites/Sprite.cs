using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Sprites
{
    public class Sprite
    {
        private Texture2D _texture;

        public Rectangle GetCollisionBox(Vector2 position)
        {
            return new Rectangle(
                        (int)position.X,
                        (int)position.Y,
                        _texture.Width,
                        _texture.Height);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;   
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool isFlippedHorizontally = false)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (isFlippedHorizontally)
            {
                flip = SpriteEffects.FlipHorizontally;
            }

            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height);
            spriteBatch.Draw(_texture, dest, null, Color.White, 0f, Vector2.Zero, flip, 0f);
        }
    }
}
