using Lario.Objects;
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
    public class LevelScene : BaseScene
    {
        private int[,] map = new int[15, 15]
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

        private int[,] collisionMap = new int[30, 30];

        private Map.Map _myMap;
        

        private Camera.Camera _camera;

        private Player.Player _player;

        private List<Objects.BaseObject> _objects = new List<Objects.BaseObject>();

        private SpriteBatch _spriteBatch;

        private bool _keyADown;

        private LevelData _levelData = new LevelData();


        private Texture2D _coinTexture;

        private SpriteFont _font;

        public LevelScene(GraphicsDevice graphics, ContentManager content) : base(graphics, content)
        {
        }

        public void Reset()
        {
            _player.Reset();

            _objects.Clear();
            LoadObjects();
        }

        private void LoadObjects()
        {
            Objects.Coin coin = new Objects.Coin(_coinTexture);
            coin.Position = new Vector2(10, 700);
            coin.Size = new Vector2(70, 70);

            _objects.Add(coin);
        }

        public override void InitializeContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var tileMapTexture = Content.Load<Texture2D>("Sprites/candy_sheet");
            var collisionTexture = Content.Load<Texture2D>("Sprites/collisions");

            _coinTexture = Content.Load<Texture2D>("Sprites/coinGold");

            _font = Content.Load<SpriteFont>("Font/Arial");

            Map.TileMapData tileMapData = new Map.TileMapData()
            {
                MapHeight = 15,
                MapWidth = 15,
                Texture = tileMapTexture,
                TileHeight = 70,
                TileWidth = 70,
                TileMap = map
            };

            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    if (map[y, x] != -1)
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

            

            _camera = new Camera.Camera();
            _camera.ViewportWidth = 1000;
            _camera.ViewportHeight = 600;

            _myMap = new Map.Map(_camera);
            _myMap.AddLayer(new Map.TileMap(tileMapData));
            _myMap.SetCollisionMap(new Map.CollisionMap(collisionMapData));




            var playerTexture = new Texture2D(GraphicsDevice, 40, 40);
            playerTexture.SetData(Enumerable.Repeat(Color.DarkSlateGray, 40 * 40).ToArray());


            _player = new Player.Player(new Vector2(500, 10), playerTexture);

            Reset();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
               SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, _camera.TranslationMatrix); ;
            _myMap.Draw(_spriteBatch);

            foreach (var obj in _objects)
            {
                obj.Draw(_spriteBatch);
            }

            _player.Draw(_spriteBatch);

            _spriteBatch.End();
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, "Score : " + _levelData.Score, new Vector2(10,10),Color.Black);
            _spriteBatch.DrawString(_font, "Life : " + _player.Life, new Vector2(10, 30), Color.Black);
            _spriteBatch.End();

        }



        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
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

            var playerCollisionBox = _player.PlayerCollisionBox;

            foreach (var obj in _objects)
            {
                obj.Update(gameTime);

                if(obj.IsCollisionWith(playerCollisionBox))
                {
                    OnCollisionBetween(_player, obj);
                }
            }

            _objects.RemoveAll(o => o.IsRemoved);
        }

        private void OnCollisionBetween(Player.Player player, BaseObject obj)
        {
            obj.OnCollision();
            AffectLevelByGameObject(obj.LevelUpdateData);
            AffectPlayerByGameObject(obj.LevelUpdateData);
            if (obj.IsRemovedOnCollision)
            {
                obj.IsRemoved = true;
            }
        }

        private void AffectPlayerByGameObject(ObjectData levelUpdateData)
        {
            _player.AffectLife(levelUpdateData.PlayerLife);
        }

        private void AffectLevelByGameObject(ObjectData obj)
        {
            _levelData.Score += obj.Score;
        }
    }
}
