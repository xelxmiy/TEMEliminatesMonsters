using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.EnemySystems.Husk;

namespace TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;

public class HuskFactory : IEntityFactory
{
	private readonly World _world;
	private readonly Texture2D _huskTexture;

	public HuskFactory (World world, Texture2D texture)
	{
		_world = world;
		_huskTexture = texture;
	}

	/// <summary>
	/// creats a new Husk entity
	/// </summary>
	/// <param name="position">position this husk spawns at</param>
	/// <returns>the created husk</returns>
	public Entity Create (Vector2 position)
	{
		//Creates a husk entity and attack a texture
		Entity husk = _world.CreateEntity();
		husk.Attach(_huskTexture);

		// add systems here
		husk.Attach(new HuskMovementSystem(position));
		// TODO: Add a health system
		// TODO: Add an attack system so they can bite things

		return husk;
	}
}
