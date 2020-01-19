using Lario.Map;
using Microsoft.Xna.Framework;
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
        private const int MaxLife = 3;

        private Vector2 _initialPosition;
        private Vector2 _position;
        private Vector2 _velocity;

        private Texture2D _playerSprite;

        private readonly Vector2 Gravity = new Vector2(0, 9.8f);

        private bool _isOnGround;

        public int Life { get; private set; } = MaxLife;

        public bool IsAlive
        {
            get { return Life > 0; }
        }

        private bool IsInvinsible { get; set; }

        private DateTime _startInvinsibleTime;

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
                        _playerSprite.Width,
                        _playerSprite.Height);
            }
        }

        public Player(Vector2 position, Texture2D playerSprite)
        {
            _position = position;
            _initialPosition = Position;


            _playerSprite = playerSprite;
        }

        public void Reset()
        {
            _position = new Vector2(_initialPosition.X, _initialPosition.Y);
            Life = MaxLife;
        }

        public void Update(GameTime gameTime, Map.Map worldMap)
        {
            HandleKeyboard();

            HandleVelocity(gameTime);

            HandleCollisionsWithWorld(worldMap);
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
                _velocity.Y = Math.Min(_velocity.Y, 10);
            }
            else
            {
                _velocity.Y = Math.Max(_velocity.Y, -10);
            }

            _velocity.X *= 0.925f;
        }

        private void HandleCollisionsWithWorld(Map.Map worldMap)
        {
            //check for collision
            var playerCollisionBox = new Rectangle(
                (int)_position.X + (int)_velocity.X,
                (int)_position.Y + (int)_velocity.Y,
                _playerSprite.Width,
                _playerSprite.Height);

            if (_velocity.X < 0 && !worldMap.IsCollisionLeft(playerCollisionBox))
            {
                _position.X += _velocity.X;

            }

            if (_velocity.X > 0 && !worldMap.IsCollisionRight(playerCollisionBox))
            {
                _position.X += _velocity.X;
            }

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
            }
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
                _velocity.Y -= 7.0f;
                _isOnGround = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerSprite, _position, Color.White);
        }

        public void AffectLife(int lifeAffected)
        {
            Life += lifeAffected;
        }

    }
}
