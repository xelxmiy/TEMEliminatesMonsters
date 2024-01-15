using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners;

namespace TEMEliminatesMonsters.src.Entities.Resource_Nodes.Spawners.Concrete;

internal class HuskSpawner : EntityUpdateSystem
{
	private FastRandom _fr = new();

	//spawner controll
	private HuskFactory _factory;
	private Vector2 _position;

	#region constants
	// husk spawning control
	private const int _maxSpawnCount = 3;
	private const int _spawnRadius = 150; // px
	#endregion

	private int _spawnCount = _maxSpawnCount;

	public HuskSpawner (HuskFactory factory, Vector2 position) : base(default)
	{
		_factory = factory;
		_position = position;
	}
	public override void Initialize (IComponentMapperService mapperService)
	{
	}

	public override void Update (GameTime gameTime)
	{
		//spawn all available enemies
		if (_spawnCount > 0)
		{
			for (int i = 0; i < _spawnCount; _spawnCount--)
			{
				_factory.Create(_position
					+ new Vector2(_fr.NextSingle(-_spawnRadius, _spawnRadius), _fr.NextSingle(-_spawnRadius, _spawnRadius))
					+ new Vector2(5, 5)); //5px offset from spawner
			}
		}

		// add 1-3 enemies to the next spawn cycle if it's been 5 seconds
		if (gameTime.GetElapsedSeconds() % 5 == 0)
		{
			_spawnCount += _fr.Next(1, 3);
		}
	}
}
