using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Collisions;
using TEMEliminatesMonsters.Src.FileLoading;
using TEMEliminatesMonsters.Src.Updateables;

namespace TEMEliminatesMonsters.Src.Map.Tiles;

//Todo: make this work + rewrite this
abstract class TrapTile : Tile, ICollisionActor
{
	/// <summary>
	/// creates a new TrapTile
	/// </summary>
	/// <param name="texture">This Tile's Texture</param>
	/// <param name="x">this tile's Position</param>
	/// <param name="y"></param>
	/// <param name="ID"></param>
	public TrapTile (GameTexture texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
	{

	}

	private IShapeF _hitBox;

	IShapeF ICollisionActor.Bounds => _hitBox;

	/// <summary>
	/// Called when an object/entity enters the hitbox
	/// </summary>
	/// <param name="collisionInfo"></param>
	public void OnCollision (CollisionEventArgs collisionInfo)
	{
		OnTrapEnter(collisionInfo.Other);
	}

	/// <summary>
	/// occurs when an object enter's the trap's hitbox
	/// </summary>
	/// <param name="object">object entering the trap</param>
	public abstract void OnTrapEnter (ICollisionActor @object);
}