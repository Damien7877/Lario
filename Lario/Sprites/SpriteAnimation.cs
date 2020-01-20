using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Sprites
{
    public class SpriteAnimation
    {
        private Texture2D[] _frames;

        private int _currentFrame;

        public int TimeBetweenFrames { get; set; }

        private double _lastFrameChange;

        public SpriteAnimation(int frameNumber)
        {
            _frames = new Texture2D[frameNumber];
        }

        public void SetFrameTexture(int frameNumber, Texture2D texture)
        {
            _frames[frameNumber] = texture;
        }

        public void SetDisplayedFrameNumber(int frameNumber)
        {
            _currentFrame = frameNumber % _frames.Length;
        }

        public void Update(GameTime gameTime)
        {
            if(TimeBetweenFrames > 0 && (gameTime.TotalGameTime.TotalMilliseconds - _lastFrameChange) > TimeBetweenFrames)
            {
                _currentFrame++;
                _currentFrame = _currentFrame % _frames.Length;
                _lastFrameChange = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool isFlippedHorizontally = false)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (isFlippedHorizontally)
            {
                flip = SpriteEffects.FlipHorizontally;
            }

            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, _frames[_currentFrame].Width, _frames[_currentFrame].Height);
            spriteBatch.Draw(_frames[_currentFrame], dest, null, Color.White, 0f , Vector2.Zero, flip, 0f);
        }
    }
}
