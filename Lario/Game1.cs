﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lario
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        Scene.BaseScene _currentScene;

        Scene.LevelScene _currentLevel;

        Scene.MenuScene _menuScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _menuScene = new Scene.MenuScene(GraphicsDevice, Content);
            _menuScene.InitializeContent();

            _menuScene.OnQuitGame += OnQuitGame;

            _menuScene.OnPlayGame += OnPlayGame;



            _currentScene = _menuScene;
        }

        private void OnPlayGame()
        {
            _currentLevel = new Scene.LevelScene(GraphicsDevice, Content);

            _currentLevel.OnPlayerDeath += OnPlayerDeath;

            _currentLevel.InitializeContent();
            _currentScene = _currentLevel;
        }

        private void OnQuitGame()
        {
            Exit();
        }

        private void OnPlayerDeath()
        {
            _currentLevel.OnPlayerDeath -= OnPlayerDeath;

            _currentScene = _menuScene;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentScene.Draw(gameTime);

            base.Draw(gameTime);

        }
    }
}
