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
	public TrapTile (GameTexture texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
	{
		if (texture.Texture != null)
		{
			_bounds = new RectangleF(new(x * 32, y * 32), new(texture.Texture.Width * texture.ScaleFactor, texture.Texture.Height * texture.ScaleFactor));
		}
	}

	private IShapeF _bounds;

	IShapeF ICollisionActor.Bounds => _bounds;

	public void OnCollision (CollisionEventArgs collisionInfo)
	{
		OnTrapEnter(collisionInfo.Other);
	}
	public abstract void OnTrapEnter (ICollisionActor @object);
}