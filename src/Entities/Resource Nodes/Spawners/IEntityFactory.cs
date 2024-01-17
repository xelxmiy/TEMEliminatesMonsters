using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;

//mandates that Factories can spawn entities
public interface IEntityFactory
{
	/// <summary>
	/// Creates an Entity
	/// </summary>
	/// <param name="position">position of the entity</param>
	/// <returns>a refrence to the entity</returns>
	public Entity Create (Vector2 position);
}