using Lario.Map;
using Lario.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Player
{
    public class Player
    {
        public const int MaxLife = 3;

        private Vector2 _initialPosition;
        private Vector2 _position;
        private Vector2 _velocity;

        private PlayerState _state;
        private Texture2D _idleTexture;
        private SpriteAnimation _runAnimation;
        private Texture2D _jumpTexture;

        private bool isTextureFlip;

        private readonly Vector2 Gravity = new Vector2(0, 9.8f);

        private bool _isOnGround;

        


        public int Life { get; private set; } = MaxLife;

        public bool IsAlive
        {
            get { return Life > 0; }
        }



        public Vector2 Position
        {
            get
            {
                return new Vector2(_position.X, _position.Y);
            }
        }

        public Rectangle PlayerCollisionBox
        {
            get
            {
                return new Rectangle(
                        (int)_position.X,
                        (int)_position.Y,
                        _idleTexture.Width,
                        _idleTexture.Height);
            }
        }

        public bool HasMoved { get; private set; }

        public Player(Vector2 position)
        {
            _position = position;
            _initialPosition = Position;
            _state = PlayerState.Idle;
        }

        public void Initialize(ContentManager content)
        {
            //Idle texture
            _idleTexture = content.Load<Texture2D>("PlayerAnimation/Male/player_idle");

            //Run animation
            var run1 = content.Load<Texture2D>("PlayerAnimation/Male/player_walk1");
            var run2 = content.Load<Texture2D>("PlayerAnimation/Male/player_walk2");
            _runAnimation = new SpriteAnimation(2);
            _runAnimation.TimeBetweenFrames = 150;
            _runAnimation.SetFrameTexture(0, run1);
            _runAnimation.SetFrameTexture(1, run2);

            //Jump texture
            _jumpTexture = content.Load<Texture2D>("PlayerAnimation/Male/player_jump");
        }

        public void Reset()
        {
            _position = new Vector2(_initialPosition.X, _initialPosition.Y);
            Life = MaxLife;
        }

        public void Update(GameTime gameTime, Map.Map worldMap)
        {
            HasMoved = false;

            HandleKeyboard();

            HandleVelocity(gameTime);

            HandleCollisionsWithWorld(worldMap);

            HandlePlayerOutbound(worldMap);

            if(_state == PlayerState.Run)
            {
                _runAnimation.Update(gameTime);
            }

            if (Math.Abs(_velocity.X) > 0.5f && _isOnGround)
            {
                _state = PlayerState.Run;
                
            }
            else if(_isOnGround)
            {
                _state = PlayerState.Idle;
            }
            else
            {
                _state = PlayerState.Jump;
            }
        }

        private void HandlePlayerOutbound(Map.Map worldMap)
        {
            if(worldMap.IsOutOfBound(Position))
            {
                Life = 0;
            }
        }

        private void HandleVelocity(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _velocity += Gravity * time;


            if (_velocity.X >= 0)
            {
                _velocity.X = Math.Min(_velocity.X, 10);
            }
            else
            {
                _velocity.X = Math.Max(_velocity.X, -10);
            }

            if (_velocity.Y >= 0)
            {
                _velocity.Y = Math.Min(_velocity.Y, 8);
            }
            else
            {
                _velocity.Y = Math.Max(_velocity.Y, -8);
            }



            _velocity.X *= 0.95f;

            //Used to avoid jittering mouvment on player
            if(Math.Abs(_velocity.X) < 0.4)
            {
                _velocity.X = 0;
            }
        }

        private void HandleCollisionsWithWorld(Map.Map worldMap)
        {
            //check for collision
            var playerCollisionBox = new Rectangle(
                (int)_position.X + (int)_velocity.X,
                (int)_position.Y + (int)_velocity.Y,
                _idleTexture.Width,
                _idleTexture.Height);

            if (_velocity.Y < 0 && worldMap.IsCollisionUp(playerCollisionBox))
            {
                _velocity.Y = 0;
            }


            if (_velocity.Y > 0 && worldMap.IsCollisionDown(playerCollisionBox))
            {
                _velocity.Y = 0;
                _isOnGround = true;
            }
            else
            {
                _position.Y += _velocity.Y;
                _isOnGround = false;
                HasMoved = true;
            }

            if (_velocity.X < 0)
            {
                if (!worldMap.IsCollisionLeft(playerCollisionBox))
                {
                    _position.X += _velocity.X;
                    HasMoved = true;
                }
            }


            if (_velocity.X > 0 )
            {
                if (!worldMap.IsCollisionRight(playerCollisionBox))
                {
                    _position.X += _velocity.X;
                    HasMoved = true;
                }
            }




            _position.Y = (float)Math.Ceiling(_position.Y);
            _position.X = (float)Math.Ceiling(_position.X);
        }

        private void HandleKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _velocity.X -= 0.5f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _velocity.X += 0.5f;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && _isOnGround)
            {
                _velocity.Y -= 8.0f;
                _isOnGround = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            switch(_state)
            {
                case PlayerState.Idle:
                    spriteBatch.Draw(_idleTexture, _position, Color.White);
                    break;
                case PlayerState.Jump:
                    spriteBatch.Draw(_jumpTexture, _position, Color.White);
                    break;
                case PlayerState.Run:
                    _runAnimation.Draw(spriteBatch, _position);
                    break;
            }
            
        }

        public void AffectLife(int lifeAffected)
        {
            Life += lifeAffected;
        }

        public void Jump(float jumpForce)
        {
            _velocity.Y -= jumpForce;
            _isOnGround = false;
        }
    }
}
