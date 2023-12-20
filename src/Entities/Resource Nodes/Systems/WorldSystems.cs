using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.AbstractSystems;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.EnemySystems.Husk;

namespace TEMEliminatesMonsters.src.Entities.Resource_Nodes.Systems
{
    public class WorldUpdateSystem : EntityUpdateSystem
    {
        public WorldUpdateSystem() : base(new())
        {
        }

        ComponentMapper<HuskMovementSystem> _movementSystemMapper;

        /// <summary>
        /// initializes this system, called automatically
        /// </summary>
        /// <param name="componentMapperService">component mapper, maps components</param>
        public override void Initialize(IComponentMapperService componentMapperService)
        {
            _movementSystemMapper = componentMapperService.GetMapper<HuskMovementSystem>();
        }

        /// <summary>
        /// updates all entities in the world
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (int id in ActiveEntities)
            {
                _movementSystemMapper.Get(id).Update(gameTime);
            }
        }
    }

    public class WorldRenderSystem : EntityDrawSystem
    {
        private SpriteBatch _spriteBatch;
        private ComponentMapper<Texture2D> _textureMapper;
        private ComponentMapper<Transform2> _transformMapper;

        /// <summary>
        /// Creates a WorldRenderSystem
        /// </summary>
        public WorldRenderSystem() : base(new())
        {
        }

        /// <summary>
        /// draws all entites in the EntityManager
        /// </summary>
        /// <param name="gameTime">current gametime</param>
        public override void Draw(GameTime gameTime)
        {
            TEM.Instance.GraphicsDevice.Clear(Color.Magenta);
            foreach (int id in ActiveEntities)
            {

                Texture2D entityTexture = _textureMapper.Get(id);
                if (entityTexture is null)
                {
                    throw new NullReferenceException($"entityTexture is null! id: {entityTexture}");
                }
                Vector2 entityPosition = _transformMapper.Get(id).Position;

                _spriteBatch.Draw(entityTexture, entityPosition, Color.White);
            }
        }

        /// <summary>
        /// initializes this system, called automatically
        /// </summary>
        /// <param name="componentMapperService">mapper service to map components</param>
        public override void Initialize(IComponentMapperService componentMapperService)
        {
            // this is bad for several reasons, but it works for now and i promise i'll fix it later
            // see the funny thing is if you're reading this it means i haven't fixed it, feel free to yell at me if so
            _spriteBatch = TEM.Instance.SpriteBatch; 
            _textureMapper = componentMapperService.GetMapper<Texture2D>();
            _transformMapper = componentMapperService.GetMapper<Transform2>();
        }
    }
}
