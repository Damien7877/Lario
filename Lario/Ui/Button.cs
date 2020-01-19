using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Ui
{
    public class Button
    {
        public Vector2 Position { get; set; }


        private Texture2D _textureNormal;

        private Texture2D _texturePressed;

        public bool IsPressed { get; private set; }

        public delegate void OnClickedHandler(object sender);

        // Declare the event.
        public event OnClickedHandler OnClicked;


        public Button(Texture2D textureNormal, Texture2D texturePressed)
        {
            _textureNormal = textureNormal;
            _texturePressed = texturePressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsPressed)
            {
                spriteBatch.Draw(_texturePressed, Position, Color.White);
            }
            else
            {
                spriteBatch.Draw(_textureNormal, Position, Color.White);
            }
            
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            Rectangle boundingBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                _textureNormal.Width,
                _textureNormal.Height);

            if(boundingBox.Contains(state.Position))
            {
                if (IsPressed && state.LeftButton == ButtonState.Released)
                {
                    OnClicked?.Invoke(this);
                }


                IsPressed = state.LeftButton == ButtonState.Pressed;
                                
            }
        }
    }
}
