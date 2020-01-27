using Lario.Events;
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
    public class FlyingEnemy : BaseEnemy
    {
     
        private Texture2D _deadTexture;

        private SpriteAnimation _flyingAnimation;

        private bool _isDirectionLeft;

        

        private TimedEvent _timedIdle;


        public FlyingEnemy(Texture2D deadTexture, SpriteAnimation flyingAnimation) : base()
        {
            _deadTexture = deadTexture;

            _flyingAnimation = flyingAnimation;

            LevelUpdateData = new ObjectData
            {
                PlayerLife = -1
            };

            _timedIdle = new TimedEvent(2000, () => _state = State.Moving);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch(_state)
            {
                case State.Idle:
                    _flyingAnimation.TimeBetweenFrames = 400;
                    _flyingAnimation.Draw(spriteBatch, Position, _isDirectionLeft);
                    break;
                case State.Moving:
                    _flyingAnimation.TimeBetweenFrames = 200;
                    _flyingAnimation.Draw(spriteBatch, Position, _isDirectionLeft);
                    break;

                case State.Dead:
                    spriteBatch.Draw(_deadTexture, Position, Color.White);
                    break;
            }
        }

        public override CollisionDirection IsCollisionWith(Rectangle collisionBox, double collisionDirection)
        {
            Rectangle enemyCollisionBox = new Rectangle(
                (int)Position.X + ((int)Size.X / 2),
                (int)Position.Y + ((int)Size.Y / 2),
                (int)Size.X / 2,
                (int)Size.Y / 2);

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
            _timedIdle.Update(gameTime.TotalGameTime.TotalMilliseconds);
            UpdateWaypoint(gameTime);

            if (_state != State.Dead)
            {
                _flyingAnimation.Update(gameTime);
            }
        }

        private void UpdateWaypoint(GameTime gameTime)
        {
            if (_currentWaypoint.HasValue)
            {

                if (Vector2.Distance(Position, _waypoints[_currentWaypoint.Value]) < 0.1 && _state == State.Moving)
                {
                    _state = State.Idle;
                    Position = _waypoints[_currentWaypoint.Value];
                    _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;
                    _timedIdle.Start(gameTime.TotalGameTime.TotalMilliseconds);
                }
                else if (_state == State.Moving)
                {
                    var direction = (_waypoints[_currentWaypoint.Value] - Position) / 100;
                    direction.Normalize();

                    if (direction.X < 0)
                    {
                        _isDirectionLeft = false;
                    }
                    else
                    {
                        _isDirectionLeft = true;
                    }

                    Position += direction;
                }
            }
        }

        public override bool IsRemovedOnCollision(CollisionDirection collisionDirection)
        {
            return collisionDirection == CollisionDirection.Down;
        }

        
    }
}
