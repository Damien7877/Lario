using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Objects
{
    public abstract class BaseObject
    {

        public Guid ObjectId { get; private set; }
        

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public bool IsRemovedOnCollision { get; protected set; }

        public bool IsRemoved { get; set; }

        protected Texture2D _texture;

        public ObjectData LevelUpdateData { get; protected set; }

        protected BaseObject(Texture2D texture)
        {
            _texture = texture;
            ObjectId = Guid.NewGuid();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract bool IsCollisionWith(Rectangle collisionBox);

        public abstract void OnCollision();

        public override string ToString()
        {
            return ObjectId.ToString();
        }

        
    }
}
