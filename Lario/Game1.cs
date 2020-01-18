using Microsoft.Xna.Framework;
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
        SpriteBatch spriteBatch;

        int[,] map = new int[15, 15]
            {
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,24,24,24,24,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,24,24,24,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,24,24,24,24,-1,-1,-1,-1,-1 },
                { -1,-1,24,24,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                { 31,31,31,31,31,31,31,31,31,31,31,31,31,31,31 },
                { 44,44,44,44,44,44,44,44,44,44,44,44,44,44,44 },
                { 44,44,44,44,44,44,44,44,44,44,44,44,44,44,44 },
                { 44,44,44,44,44,44,44,44,44,44,44,44,44,44,44 },

            };

        int[,] collisionMap = new int[30, 30];

        Map.Map _myMap;
        private bool _keyADown;

        Camera.Camera _camera;

        Player.Player _player;

        List<Objects.BaseObject> _objects = new List<Objects.BaseObject>();

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

            var coinTexture = Content.Load<Texture2D>("Sprites/coinGold");

            Map.TileMapData tileMapData = new Map.TileMapData()
            {
                MapHeight = 15,
                MapWidth = 15,
                Texture = tileMapTexture,
                TileHeight  = 70,
                TileWidth = 70,
                TileMap = map
            };

            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    if(map[y,x] != -1)
                    {
                        collisionMap[y * 2, x * 2] = 1;
                        collisionMap[y * 2, x * 2 + 1] = 1;
                    }
                    
                }
            }

            Map.TileMapData collisionMapData = new Map.TileMapData()
            {
                MapHeight = 30,
                MapWidth = 30,
                Texture = collisionTexture,
                TileHeight = 35,
                TileWidth = 35,
                TileMap = collisionMap
            };

            Rectangle worldSize = new Rectangle(0, 0, tileMapData.MapWidth * tileMapData.TileWidth, tileMapData.MapHeight * tileMapData.TileHeight);

            _camera = new Camera.Camera();
            _camera.ViewportWidth = 1000;
            _camera.ViewportHeight = 600;

            _myMap = new Map.Map(_camera);
            _myMap.AddLayer(new Map.TileMap(tileMapData));
            _myMap.SetCollisionMap(new Map.CollisionMap(collisionMapData));

            Objects.Coin coin = new Objects.Coin(coinTexture);
            coin.Position = new Vector2(10, 700);
            _objects.Add(coin);


            var playerTexture = new Texture2D(GraphicsDevice, 40, 40);
            playerTexture.SetData(Enumerable.Repeat(Color.DarkSlateGray, 40*40).ToArray());
           

            _player = new Player.Player(new Vector2(500, 10), playerTexture);
            
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

            _player.Update(gameTime, _myMap);
            _camera.CenterOn(_player.Position);

            foreach(var obj in _objects)
            {
                obj.Update(gameTime);
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

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, _camera.TranslationMatrix); ;
            _myMap.Draw(spriteBatch);
            
            foreach (var obj in _objects)
            {
                obj.Draw(spriteBatch);
            }

            _player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);

            double drawTime = gameTime.ElapsedGameTime.TotalMilliseconds - startTime ;
        }
    }
}
