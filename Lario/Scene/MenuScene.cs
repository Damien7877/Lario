using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Scene
{
    public class MenuScene : BaseScene
    {
        
        public delegate void QuitGameHandler();

        // Declare the event.
        public event QuitGameHandler OnQuitGame;

        public delegate void PlayGameHandler();

        // Declare the event.
        public event PlayGameHandler OnPlayGame;

        private Ui.Button _quitButton;

        private Ui.Button _playButton;

        private SpriteBatch _spriteBatch;

        public MenuScene(GraphicsDevice graphicsDevice, ContentManager content) : base(graphicsDevice, content)
        {
        }

        public override void InitializeContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CreateQuitButton();

            CreatePlayButton();
        }

        private void CreatePlayButton()
        {
            Texture2D playButtonNormal = Content.Load<Texture2D>("Sprites/ui_play_normal");
            Texture2D playButtonPressed = Content.Load<Texture2D>("Sprites/ui_play_pressed");
            _playButton = new Ui.Button(playButtonNormal, playButtonPressed);

            _playButton.Position = new Vector2(
                GraphicsDevice.Viewport.Width / 2 - playButtonNormal.Width / 2,
                GraphicsDevice.Viewport.Height / 2 - playButtonNormal.Height / 2);

            _playButton.OnClicked += obj => OnPlayGame?.Invoke();
        }

        private void CreateQuitButton()
        {
            Texture2D quitButtonNormal = Content.Load<Texture2D>("Sprites/ui_quit_normal");
            Texture2D quitButtonPressed = Content.Load<Texture2D>("Sprites/ui_quit_pressed");
            _quitButton = new Ui.Button(quitButtonNormal, quitButtonPressed);

            _quitButton.Position = new Vector2(
                GraphicsDevice.Viewport.Width / 2 - quitButtonNormal.Width / 2,
                GraphicsDevice.Viewport.Height / 2 - quitButtonNormal.Height / 2 + 100);

            _quitButton.OnClicked += (obj) => OnQuitGame?.Invoke();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _quitButton.Draw(_spriteBatch);
            _playButton.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                OnQuitGame?.Invoke();

            _quitButton.Update(gameTime);
            _playButton.Update(gameTime);
        }

        public override void Unload()
        {
            
        }
    }
}
