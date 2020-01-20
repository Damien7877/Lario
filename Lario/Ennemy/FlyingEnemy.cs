using Lario.Objects;
using Lario.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Ennemy
{
    public class FlyingEnemy : BaseObject
    {
        private enum State
        {
            Idle,
            Flying,
            Dead
        }

        private Texture2D _deadTexture;

        private SpriteAnimation _flyingAnimation;

        private State _state;

        public FlyingEnemy(Texture2D deadTexture, SpriteAnimation flyingAnimation) : base()
        {
            _deadTexture = deadTexture;

            _flyingAnimation = flyingAnimation;

            _state = State.Idle;

            LevelUpdateData = new ObjectData
            {
                PlayerLife = -1
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch(_state)
            {
                case State.Idle:
                    _flyingAnimation.TimeBetweenFrames = 400;
                    _flyingAnimation.Draw(spriteBatch, Position, true);
                    break;
                case State.Flying:
                    _flyingAnimation.TimeBetweenFrames = 200;
                    _flyingAnimation.Draw(spriteBatch, Position);
                    break;

                case State.Dead:
                    spriteBatch.Draw(_deadTexture, Position, Color.White);
                    break;
            }
        }

        public override CollisionDirection IsCollisionWith(Rectangle collisionBox, double collisionDirection)
        {
            Rectangle enemyCollisionBox = new Rectangle(
                (int)Position.X + ((int)Size.X / 3),
                (int)Position.Y + ((int)Size.Y / 3),
                (int)Size.X / 3,
                (int)Size.Y / 3);

            if(enemyCollisionBox.Intersects(collisionBox))
            {
                if (collisionDirection > 25 && collisionDirection < 130)
                {
                    LevelUpdateData.PlayerLife = 0;
                    LevelUpdateData.PlayerJump = true;
                    LevelUpdateData.JumpForce = 13;
                    return CollisionDirection.Down;
                }
                else
                {
                    return CollisionDirection.All;
                }
            }


            return CollisionDirection.None;
        }

        public override void OnCollision(CollisionDirection collisionDirection)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(_state != State.Dead)
            {
                _flyingAnimation.Update(gameTime);
            }
        }

        public override bool IsRemovedOnCollision(CollisionDirection collisionDirection)
        {
            return collisionDirection == CollisionDirection.Down;
        }
    }
}
