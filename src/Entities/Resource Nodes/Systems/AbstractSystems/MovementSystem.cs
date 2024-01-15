using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;
using System.Diagnostics;

namespace TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.AbstractSystems;

abstract class MovementSystem : EntityUpdateSystem
{

	protected MovementSystem (Transform2 bounds) : base(new())
	{
		Position = bounds.Position;
	}

	public Vector2 Position { get; protected set; }

	private protected Vector2 _target;
	protected abstract float MovementSpeed { get; set; }

	/// <summary>
	/// runs continously, updates this enitity
	/// </summary>
	/// <param name="gameTime">current game time</param>
	public override void Update (GameTime gameTime)
	{
		Vector2 mv = GetMovementDirection();
		if (CanMove(mv))
			Move(mv);
	}

	/// <summary>
	/// displaces this entity
	/// </summary>
	/// <param name="mv">vector to displace</param>
	protected abstract void Move (Vector2 mv);

	/// <summary>
	/// verifies that this entity is allowed to move in the desired direction
	/// </summary>
	/// <param name="mv">movement vector</param>
	/// <returns>if the movement is allowed</returns>
	protected abstract bool CanMove (Vector2 mv);
	/// <summary>
	/// finds a vector to approach the target
	/// </summary>
	/// <returns>a movement vector towards the location of the target</returns>
	protected abstract Vector2 GetMovementDirection ();

	/// <summary>
	/// sets the target for this entity to move towards
	/// </summary>
	/// <param name="target">the target's location</param>
	public void SetTarget (Vector2 target)
	{
		_target = target;
	}

}
