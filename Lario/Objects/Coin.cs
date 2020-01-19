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

        public Coin(Texture2D texture) : base(texture)
        {
            LevelUpdateData = new ObjectData
            {
                PlayerLife = 0,
                Score = 1,
            };

            IsRemovedOnCollision = true;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override bool IsCollisionWith(Rectangle collisionBox)
        {
            Rectangle coinCollisionBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Position.X + (int)Size.X,
                (int)Position.Y + (int)Size.Y);


            return coinCollisionBox.Intersects(collisionBox);
        }

        public override void OnCollision()
        {
            //Play sound Collected
        }
    }
}
