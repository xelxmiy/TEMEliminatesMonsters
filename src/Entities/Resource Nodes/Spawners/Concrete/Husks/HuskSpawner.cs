using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;

namespace TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners.Concrete;

internal class HuskSpawner : EntityUpdateSystem, IEntitySpawner
{
	private readonly FastRandom _fastRandom = new();

	//spawner controll

	private Vector2 _position;
	private HuskFactory _factory;

	#region constants
	// husk spawning control
	private const int _maxSpawnCount = 3;
	private const int _spawnRadius = 150; // px
	#endregion

	private int _spawnCount = _maxSpawnCount;

	public Vector2 Position => _position;
	public IEntityFactory Factory 
	{ 
		get => _factory;
		private set => _factory = (HuskFactory)value;
	}
	public HuskSpawner (HuskFactory factory, Vector2 position) : base(default)
	{
		_factory = factory;
		_position = position;
	}

	public override void Initialize (IComponentMapperService mapperService) { }

	/// <summary>
	/// Update this Spawner
	/// </summary>
	/// <param name="gameTime">current game time</param>
	public override void Update (GameTime gameTime)
	{
		//spawn all available enemies
		if (_spawnCount > 0)
		{
			for (int i = 0; i < _spawnCount; _spawnCount--)
			{
				Entity ent = Factory.Create(_position
					+ new Vector2(_fastRandom.NextSingle(-_spawnRadius, _spawnRadius), _fastRandom.NextSingle(-_spawnRadius, _spawnRadius))
					+ new Vector2(5, 5)); //5px offset from spawner

			}
		}

		// add 1-3 enemies to the next spawn cycle if it's been 5 seconds
		if (gameTime.TotalGameTime.TotalSeconds % 5 < 0.02)
		{
			_spawnCount += _fastRandom.Next(1, 3);
		}
	}
}
