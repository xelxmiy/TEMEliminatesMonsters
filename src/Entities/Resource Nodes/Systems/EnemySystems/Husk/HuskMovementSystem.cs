using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using System;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.AbstractSystems;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.EnemySystems.Husk;
internal class HuskMovementSystem : MovementSystem
{
    /// <summary>
    /// Creates a Husk Movement System based on this husk's location
    /// </summary>
    /// <param name="bounds"></param>
    public HuskMovementSystem(Transform2 bounds) : base(bounds) {}

    protected override float MovementSpeed { get; set; } = 1.3f;

    public override void Initialize(IComponentMapperService mapperService) 
    {
    
    } 

    /// <summary>
    /// Keeps the Husk in-bounds
    /// </summary>
    /// <param name="movementVector">verifies that the movement vector keeps the husk in-bounds</param>
    /// <returns></returns>
    protected override bool CanMove(Vector2 movementVector) // to be expanded upon
    {
        if (Position.X < 0)
        {
            Position = new Vector2(0, Position.Y);
        }
        if (Position.Y < 0)
        {
            Position = new Vector2(Position.X, 0);
        }
        if (Position.X > TEM.Instance.Map.GridWidth)
        {
            Position = new Vector2(TEM.Instance.Map.GridWidth, Position.Y);
        }
        if (Position.Y > TEM.Instance.Map.GridLength)
        {
            Position = new Vector2(Position.X, TEM.Instance.Map.GridLength);
        }
        return true;
    }
    /// <summary>
    /// returns the direction for this husk to move (very simple straight line)
    /// </summary>
    /// <returns></returns>
    protected override Vector2 GetMovementDirection() 
    {
        Vector2 Difference = _target - Position;

        if (Difference == Vector2.Zero) return Vector2.Zero; //can't normalize the zero vector :(

        Vector2 movementDirection = Vector2.Normalize(Difference);

        return movementDirection * MovementSpeed;
    }

    /// <summary>
    /// changes the position of this husk based on the movement vector
    /// </summary>
    /// <param name="movementVector">direction to move this husk</param>
    protected override void Move(Vector2 movementVector) 
    {
        Position += movementVector;
    }
}
