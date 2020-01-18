using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lario
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int[,] map = new int[7, 15]
            {
                { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,2 },
                { 14,15,16,17,18,19,20,21,22,23,24,25,26,27,2 },
                { 28,29,30,31,32,33,34,35,36,37,38,39,40,41,2 },
                { 42,43,44,45,46,47,48,49,50,51,52,53,54,55,2 },
                { 56,57,58,59,60,61,62,63,64,65,66,67,68,69,2 },
                { 70,71,72,73,74,75,76,77,78,79,80,81,82,83,2 },
                { 84,85,86,87,88,89,90,91,92,93,94,95,96,97,2 },
            };

        int[,] collisionMap = new int[7*2, 15*2]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },

            };

        Map.Map _myMap;
        private bool _keyADown;

        Camera.Camera _camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
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
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var tileMapTexture = Content.Load<Texture2D>("Sprites/candy_sheet");
            var collisionTexture = Content.Load<Texture2D>("Sprites/collisions");

            Map.TileMapData tileMapData = new Map.TileMapData()
            {
                MapHeight = 7,
                MapWidth = 15,
                Texture = tileMapTexture,
                TileHeight  = 70,
                TileWidth = 70,
                TileMap = map
            };

            Map.TileMapData collisionMapData = new Map.TileMapData()
            {
                MapHeight = 7*2,
                MapWidth = 15*2,
                Texture = collisionTexture,
                TileHeight = 70/2,
                TileWidth = 70/2,
                TileMap = collisionMap
            };


            Rectangle worldSize = new Rectangle(0, 0, tileMapData.MapWidth * tileMapData.TileWidth, tileMapData.MapHeight * tileMapData.TileHeight);

            _camera = new Camera.Camera(worldSize, new Vector2(1000, 600));

            _myMap = new Map.Map(_camera);
            _myMap.AddLayer(new Map.TileMap(tileMapData));
            _myMap.SetCollisionMap(new Map.TileMap(collisionMapData));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _keyADown = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.A) && _keyADown)
            {
                _myMap.IsDrawCollisionMap = !_myMap.IsDrawCollisionMap;

                _keyADown = false;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _camera.Move(new Vector2(0, 1));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _camera.Move(new Vector2(0, -1));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _camera.Move(new Vector2(-1, 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _camera.Move(new Vector2(1, 0));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            double startTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            _myMap.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

            double drawTime = gameTime.ElapsedGameTime.TotalMilliseconds - startTime ;
        }
    }
}
