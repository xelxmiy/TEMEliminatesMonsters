using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using System;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.AbstractSystems;

namespace TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.EnemySystems.Husk;
internal class HuskMovementSystem : MovementSystem
{
	/// <summary>
	/// Creates a Husk Movement System based on this husk's location
	/// </summary>
	/// <param name="position"></param>
	public HuskMovementSystem (Vector2 position) : base(position) { }

	protected override float MovementSpeed { get; set; } = 130f;

	/// <summary>
	/// initializes this system
	/// </summary>
	/// <param name="mapperService">mapper serverices to map components</param>
	public override void Initialize (IComponentMapperService mapperService)
	{
		MovementSpeed += Random.Shared.NextSingle(-20, 20);
	}

	/// <summary>
	/// Updates this husk
	/// </summary>
	/// <param name="gameTime">current gametime</param>
	public override void Update (GameTime gameTime)
	{
		UpdateTargetPosition();
		Vector2 mv = GetMovementDirection();
		if (CanMove(mv))
			Move(mv * gameTime.GetElapsedSeconds());
	}

	/// <summary>
	/// updates the movement target for this husk
	/// </summary>
	private void UpdateTargetPosition ()
	{
		_target = TEM.MousePosition;
	}

	/// <summary>
	/// Keeps the Husk in-position
	/// </summary>
	/// <param name="movementVector">verifies that the movement vector keeps the husk in-position</param>
	/// <returns></returns>
	protected override bool CanMove (Vector2 movementVector) // to be expanded upon
	{
		return true;
	}
	/// <summary>
	/// returns the direction for this husk to move (very simple straight line)
	/// </summary>
	/// <returns></returns>
	protected override Vector2 GetMovementDirection ()
	{
		Vector2 difference = _target - Position;

		if (difference == Vector2.Zero) return Vector2.Zero; //can't normalize the zero vector :(
		Vector2 movementDirection = Vector2.Normalize(difference);

		return movementDirection * MovementSpeed;
	}

	/// <summary>
	/// changes the position of this husk based on the movement vector
	/// </summary>
	/// <param name="movementVector">direction to move this husk</param>
	protected override void Move (Vector2 movementVector)
	{
		Position += movementVector;
	}
}
