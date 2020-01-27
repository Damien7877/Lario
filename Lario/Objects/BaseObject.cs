using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Objects
{
    public enum CollisionDirection
    {
        Up,
        Down,
        Left,
        Right,

        None,
        All
    }

    public abstract class BaseObject
    {

        public Guid ObjectId { get; private set; }
        

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        

        public bool IsRemoved { get; set; }



        public ObjectData LevelUpdateData { get; protected set; }

        protected BaseObject()
        {
            ObjectId = Guid.NewGuid();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collisionBox"></param>
        /// <param name="collisionDirection">Angle of collision of the object</param>
        /// <returns></returns>
        public abstract CollisionDirection IsCollisionWith(Rectangle collisionBox, double collisionDirection);

        public abstract void OnCollision(GameTime gameTime, CollisionDirection collisionDirection);

        public abstract bool IsRemovedOnCollision(CollisionDirection collisionDirection);

        public override string ToString()
        {
            return ObjectId.ToString();
        }

        
    }
}
