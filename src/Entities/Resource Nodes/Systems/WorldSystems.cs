using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;

namespace TEMEliminatesMonsters.src.Entities.Resource_Nodes.Systems
{
    public class WorldUpdateSystem : IUpdateSystem
    {
        /// <summary>
        /// disposes of this system 
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// initializes this system, called automatically
        /// </summary>
        /// <param name="world">World this system belongs to</param>
        public void Initialize(World world) { }

        /// <summary>
        /// updates all entities in the world
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            EntityManager entityManager = TEM.Instance.EntityManager;
            foreach (int id in entityManager.Entities)
            {
                Entity entity = entityManager.Get(id);
                entity.Get<EntityUpdateSystem>().Update(gameTime);
            }
        }
    }

    public class WorldRenderSystem : IDrawSystem
    {
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// disposes of this system 
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// draws all entites in the EntityManager
        /// </summary>
        /// <param name="gameTime">current gametime</param>
        public void Draw(GameTime gameTime)
        {
            EntityManager entityManager = TEM.Instance.EntityManager;
            foreach (int id in entityManager.Entities)
            {
                Entity entity = entityManager.Get(id);
                Texture2D entityTexture = entity.Get<Texture2D>();
                Vector2 entityPosition = entity.Get<Transform2>().Position;

                _spriteBatch.Draw(entityTexture, entityPosition, Color.White);
            }
        }

        /// <summary>
        /// initializes this system, called automatically
        /// </summary>
        /// <param name="world">World this system belongs to</param>
        public void Initialize(World world)
        {
            _spriteBatch = new SpriteBatch(TEM.Instance.GraphicsDevice);
        }
    }
}
