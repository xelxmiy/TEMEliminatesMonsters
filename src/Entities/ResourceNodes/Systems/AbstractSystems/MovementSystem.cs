using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.AbstractSystems
{
    abstract class MovementSystem : UpdateSystem
    {

        protected MovementSystem(Transform2 bounds) 
        {
            Position = bounds.Position;
        }

        public Vector2 Position { get; protected set; }

        private protected Vector2 _target;
        protected abstract float MovementSpeed { get; set; }

        public override void Update(GameTime gameTime)
        {
            Vector2 mv = GetMovementDirection();
            if (CanMove(mv))
                Move(mv);
        }

        protected abstract void Move(Vector2 mv);
        protected abstract bool CanMove(Vector2 mv);
        protected abstract Vector2 GetMovementDirection();


        public void SetTarget(Vector2 target)
        {
            _target = target;
        }

    }
}
