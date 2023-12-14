using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners
{
    public interface IEntityFactory
    {
        protected Entity Create(Vector2 Position);

        public Entity Create(Vector3 Position) 
        {
            TEM.Instance.EntityManager.Create();

            return Create(Position);
        }
    }
}