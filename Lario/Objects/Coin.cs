using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Objects
{
    public class Coin : BaseObject
    {
        protected Texture2D _texture;
        public Coin(Texture2D texture) : base()
        {
            _texture = texture;
            LevelUpdateData = new ObjectData
            {
                PlayerLife = 0,
                Score = 1,
            };
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override CollisionDirection IsCollisionWith(Rectangle collisionBox, double collisionDirection)
        {
            Rectangle coinCollisionBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y);

            if (coinCollisionBox.Intersects(collisionBox))
            {
                return CollisionDirection.All;
            }
            return CollisionDirection.None;

        }

        public override void OnCollision(GameTime gameTime, CollisionDirection collisionDirection)
        {
            //Play sound Collected
        }

        public override bool IsRemovedOnCollision(CollisionDirection collisionDirection)
        {
            return true;
        }
    }
}
