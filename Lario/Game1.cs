using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        Map.Map myMap = new Map.Map();

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

            var tileMap = Content.Load<Texture2D>("Sprites/candy_sheet");

            Map.TileMapData tileMapData = new Map.TileMapData()
            {
                MapHeight = 7,
                MapWidth = 15,
                Texture = tileMap,
                TileHeight  = 70,
                TileWidth = 70,
                TileMap = map
            };

            myMap.AddLayer(new Map.TileMap(tileMapData));
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

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            myMap.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
