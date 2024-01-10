using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TEMEliminatesMonsters.src.Entities.Resource_Nodes.Spawners.Concrete;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.AbstractSystems;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.EnemySystems.Husk;

namespace TEMEliminatesMonsters.src.Entities.Resource_Nodes.Systems;

public class WorldUpdateSystem : EntityUpdateSystem
{
    public WorldUpdateSystem() : base(new())
    {
    }

    ComponentMapper<HuskMovementSystem> _huskMovementSystem;
	ComponentMapper<HuskSpawner> _huskSpawnerSystem;

    /// <summary>
    /// initializes this system, called automatically
    /// </summary>
    /// <param name="componentMapperService">component mapper, maps components</param>
    public override void Initialize(IComponentMapperService componentMapperService)
    {
        _huskMovementSystem = componentMapperService.GetMapper<HuskMovementSystem>();
		_huskSpawnerSystem = componentMapperService.GetMapper<HuskSpawner>();

	}

    /// <summary>
    /// updates all entities in the world
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        foreach (int id in ActiveEntities)
        {
            _huskMovementSystem.Get(id)?.Update(gameTime);
			_huskSpawnerSystem.Get(id)?.Update(gameTime);
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
                Debug.Fail($"entityTexture is null! id: {entityTexture}");
                continue;
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
        _spriteBatch = TEM.Instance.SpriteBatch;
        _textureMapper = componentMapperService.GetMapper<Texture2D>();
        _transformMapper = componentMapperService.GetMapper<Transform2>();
    }
}
