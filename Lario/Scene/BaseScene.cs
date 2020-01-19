using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Scene
{
    public abstract class BaseScene
    {
        protected GraphicsDevice GraphicsDevice { get; set; }

        protected ContentManager Content { get; set; }

        protected BaseScene(GraphicsDevice graphicsDevice, ContentManager content)
        {
            GraphicsDevice = graphicsDevice;
            Content = content;
        }

        public abstract void InitializeContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void Unload();
    }
}
