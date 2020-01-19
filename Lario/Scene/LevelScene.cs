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
                { 31,31,31,31,31,31,31,31,-1,31,31,31,31,31,31 },
                { 44,44,44,44,44,44,44,44,-1,44,44,44,44,44,44 },
                { 44,44,44,44,44,44,44,44,-1,44,44,44,44,44,44 },
                { 44,44,44,44,44,44,44,44,-1,44,44,44,44,44,44 },

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

        private Texture2D _emptyHeartHud;
        private Texture2D _fullHeartHud;

        private Texture2D _coinHud;
        private Dictionary<char, Texture2D> _numbersTexturesHud = new Dictionary<char, Texture2D>();

        private SpriteFont _font;

        public delegate void PlayerDeathHandler();

        // Declare the event.
        public event PlayerDeathHandler OnPlayerDeath;

        public LevelScene(GraphicsDevice graphics, ContentManager content) : base(graphics, content)
        {
        }

        public void Reset()
        {
            _player.Reset();
            _levelData.Score = 0;
            _objects.Clear();
            LoadObjects();
        }

        private void LoadObjects()
        {
            Objects.Coin coin = new Objects.Coin(_coinTexture);
            coin.Position = new Vector2(50, 700);
            coin.Size = new Vector2(37, 37);

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

            LoadHudTextures();

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
                    if (x == 0 || x == 14)
                    {
                        collisionMap[y * 2, x * 2] = 1;
                        collisionMap[y * 2 + 1, x * 2] = 1;
                    }
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
            _camera.ViewportWidth = GraphicsDevice.Viewport.Width;
            _camera.ViewportHeight = GraphicsDevice.Viewport.Height;

            _myMap = new Map.Map(_camera);
            _myMap.AddLayer(new Map.TileMap(tileMapData));
            _myMap.SetCollisionMap(new Map.CollisionMap(collisionMapData));





            _player = new Player.Player(new Vector2(500, 10));
            _player.Initialize(Content);
            Reset();
        }

        private void LoadHudTextures()
        {
            _emptyHeartHud = Content.Load<Texture2D>("Sprites/hud_heartEmpty");

            _fullHeartHud = Content.Load<Texture2D>("Sprites/hud_heartFull");

            _coinHud = Content.Load<Texture2D>("Sprites/hud_coins");

            for (int i = 0; i < 10; i++)
            {
                _numbersTexturesHud[Convert.ToChar(i + Convert.ToInt32('0'))] = Content.Load<Texture2D>("Sprites/hud_" + i);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend,
               SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, _camera.TranslationMatrix); ;
            _myMap.Draw(_spriteBatch);

            foreach (var obj in _objects)
            {
                obj.Draw(_spriteBatch);
            }

            _player.Draw(_spriteBatch);

            _spriteBatch.End();


            _spriteBatch.Begin();

            DrawUi(_spriteBatch, gameTime);

            _spriteBatch.End();

        }

        private void DrawUi(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 heatPosition = new Vector2(GraphicsDevice.Viewport.Width - (_emptyHeartHud.Width * 3) - 10, 10);
            for (int i = 0; i < Player.Player.MaxLife; i++)
            {
                
                if(_player.Life < i+1)
                {
                    spriteBatch.Draw(_emptyHeartHud, heatPosition, Color.White);
                }
                else
                {
                    spriteBatch.Draw(_fullHeartHud, heatPosition, Color.White);
                }
                heatPosition.X += _emptyHeartHud.Width;
            }
            Vector2 positionScore = new Vector2(10, 10);
            spriteBatch.Draw(_coinHud, positionScore, Color.White);

            positionScore.X += _coinHud.Width + 5;
            positionScore.Y += 3;
            foreach(char car in _levelData.Score.ToString().AsEnumerable())
            {
                spriteBatch.Draw(_numbersTexturesHud[car], positionScore, Color.White);
                positionScore.X += _numbersTexturesHud[car].Width;
            }

            spriteBatch.DrawString(_font, "Time : " + gameTime.ElapsedGameTime.TotalMilliseconds,new Vector2(10, 100) , Color.White);
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
            if(_player.HasMoved)
            {
                _camera.CenterOn(_player.Position);
            }
            

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

            if (!_player.IsAlive)
            {
                OnPlayerDeath?.Invoke();
            }
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
            if(levelUpdateData.PlayerJump)
            {
                _player.Jump(levelUpdateData.JumpForce);
            }
        }

        private void AffectLevelByGameObject(ObjectData obj)
        {
            _levelData.Score += obj.Score;
        }

        public override void Unload()
        {
            
        }
    }
}
