using MonoGame.Extended.Entities;
using MonoGame.Extended;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.EnemySystems.Husk;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TEMEliminatesMonsters.src.Entities.Resource_Nodes.Spawners.Concrete.Husks;

// ._. this is a factory that creates an entity that has a factory that creates a huskSpawner..
// maybe trying to teach myself patterns was a mistake
internal class HuskSpawnerFactory : IEntityFactory
{
	private readonly World _world;
	private readonly Texture2D _spawnerTexture;

	public HuskSpawnerFactory (World world, Texture2D texture)
	{
		_world = world;
		_spawnerTexture = texture;
	}

	public Entity Create (Vector2 position)
	{
		//Creates a huskSpawner entity
		Entity huskSpawner = _world.CreateEntity();
		huskSpawner.Attach(_spawnerTexture);
		huskSpawner.Attach(new Transform2(position));
		huskSpawner.Attach(new HuskSpawner(new(_world, TEM.Instance._zombie), position));
		return huskSpawner;
	}
}
