using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System.Diagnostics;
using TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.AbstractSystems;

namespace TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Systems;

internal class WorldUpdateSystem<T> : EntityUpdateSystem where T : EntityUpdateSystem
{
	public WorldUpdateSystem () : base(new())
	{
	}

	ComponentMapper<T> _entityUpdateSystem;

	/// <summary>
	/// initializes this system, called automatically
	/// </summary>
	/// <param name="componentMapperService">component mapper, maps components</param>
	public override void Initialize (IComponentMapperService componentMapperService)
	{
		_entityUpdateSystem = componentMapperService.GetMapper<T>();
	}

	/// <summary>
	/// updates all entities in the world
	/// </summary>
	/// <param name="gameTime"></param>
	public override void Update (GameTime gameTime)
	{
		foreach (int id in ActiveEntities)
		{
			_entityUpdateSystem.Get(id)?.Update(gameTime);
		}
	}
}

internal class EnemyRenderSystem<T> : EntityDrawSystem where T : MovementSystem
{
	private SpriteBatch _spriteBatch;
	private ComponentMapper<Texture2D> _textureMapper;
	private ComponentMapper<T> _movementSystemMapper;

	/// <summary>
	/// Creates a EnemyRenderSystem
	/// </summary>
	public EnemyRenderSystem () : base(new())
	{
	}

	/// <summary>
	/// draws all entites in the EntityManager
	/// </summary>
	/// <param name="gameTime">current gametime</param>
	public override void Draw (GameTime gameTime)
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

			MovementSystem movementSystem = _movementSystemMapper.Get(id);

			if (movementSystem is not null)
			{
				Vector2 entityPosition = movementSystem.Position;

				_spriteBatch.Draw(entityTexture, entityPosition, Color.White);
			}
		}
	}

	/// <summary>
	/// initializes this system, called automaticallyA
	/// </summary>
	/// <param name="componentMapperService">mapper service to map components</param>
	public override void Initialize (IComponentMapperService componentMapperService)
	{
		_spriteBatch = TEM.Instance.SpriteBatch;
		_textureMapper = componentMapperService.GetMapper<Texture2D>();
		_movementSystemMapper = componentMapperService.GetMapper<T>();
	}
}

// TODO, remove/rework this whole thing , it's literally a copy of the thing above it but only for the spawners
internal class SpawnerRenderSystem<T> : EntityDrawSystem where T : class, IEntitySpawner
{
	private SpriteBatch _spriteBatch;
	private ComponentMapper<Texture2D> _textureMapper;
	private ComponentMapper<T> _entitySpawnerMapper;

	/// <summary>
	/// Creates a EnemyRenderSystem
	/// </summary>
	public SpawnerRenderSystem () : base(new())
	{
	}

	/// <summary>
	/// draws all entites in the EntityManager
	/// </summary>
	/// <param name="gameTime">current gametime</param>
	public override void Draw (GameTime gameTime)
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

			IEntitySpawner entitySpawner = _entitySpawnerMapper.Get(id);

			if (entitySpawner is not null)
			{
				Vector2 entityPosition = entitySpawner.Position;

				_spriteBatch.Draw(entityTexture, entityPosition, Color.White);
			}
		}
	}

	/// <summary>
	/// initializes this system, called automatically
	/// </summary>
	/// <param name="componentMapperService">mapper service to map components</param>
	public override void Initialize (IComponentMapperService componentMapperService)
	{
		_spriteBatch = TEM.Instance.SpriteBatch;
		_textureMapper = componentMapperService.GetMapper<Texture2D>();
		_entitySpawnerMapper = componentMapperService.GetMapper<T>();
	}
}