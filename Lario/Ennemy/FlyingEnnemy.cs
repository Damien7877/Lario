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
    public class FlyingEnnemy : BaseObject
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

        public FlyingEnnemy(Texture2D deadTexture, SpriteAnimation flyingAnimation) : base()
        {
            _deadTexture = deadTexture;

            _flyingAnimation = flyingAnimation;

            _state = State.Idle;
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

        public override bool IsCollisionWith(Rectangle collisionBox)
        {
            return false;
        }

        public override void OnCollision()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(_state != State.Dead)
            {
                _flyingAnimation.Update(gameTime);
            }
        }
    }
}
