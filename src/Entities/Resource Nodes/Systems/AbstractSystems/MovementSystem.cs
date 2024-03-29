﻿using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;

namespace TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.AbstractSystems;

internal abstract class MovementSystem : EntityUpdateSystem
{

	protected MovementSystem (Vector2 position) : base(new())
	{
		Position = position;
	}

	public Vector2 Position { get; protected set; }

	private protected Vector2 _target;
	protected abstract float MovementSpeed { get; set; }

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
