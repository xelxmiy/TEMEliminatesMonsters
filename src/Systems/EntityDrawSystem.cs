using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Systems
{
    internal class EntityDrawSystem : IDrawSystem
    {
        private SpriteBatch _spriteBatch;

        private World _world;

        public EntityDrawSystem(SpriteBatch spriteBatch) { _spriteBatch = spriteBatch; }

        public void Initialize(World world) { _world = world; }
        public void Dispose() { }
        public void Draw(GameTime gameTime) 
        {
            
        }
    }
}
