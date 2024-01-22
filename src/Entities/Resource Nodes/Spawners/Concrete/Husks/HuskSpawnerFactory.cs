using MonoGame.Extended.Entities;
using MonoGame.Extended;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TEMEliminatesMonsters.Src.FileLoading;

namespace TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners.Concrete.Husks;

// ._. this is a factory that creates an entity that has a factory that creates a huskSpawner..
// maybe trying to teach myself patterns was a mistake

// nevermind this works amazingly, i am a god 
internal class HuskSpawnerFactory : IEntityFactory
{
	private readonly World _world;
	private readonly Texture2D _spawnerTexture;

	public HuskSpawnerFactory (World world, Texture2D texture)
	{
		_world = world;
		_spawnerTexture = texture;
	}

	/// <summary>
	/// Creats a new HuskSpawner entity
	/// </summary>
	/// <param name="position">position of spawner</param>
	/// <returns>a refrence to this husk spawner</returns>
	public Entity Create (Vector2 position)
	{
		//Creates a huskSpawner entity
		Entity huskSpawner = _world.CreateEntity();
		huskSpawner.Attach(_spawnerTexture);
		huskSpawner.Attach(new Transform2(position));

		// Add systems here
		huskSpawner.Attach(new HuskSpawner(new(_world, FileManager.Enemies["Enemies\\Husk"]), position));

		return huskSpawner;
	}
}
