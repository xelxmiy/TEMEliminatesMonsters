using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Entities.WorldSystems
{
    internal class EntityRenderSystem : EntityDrawSystem
    {
        private SpriteBatch _spriteBatch;

        private World _world;

        public EntityRenderSystem(GraphicsDevice graphicsDevice) : base(Aspect.All())
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {

        }
    }
}
