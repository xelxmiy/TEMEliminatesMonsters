using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners
{
    public interface IEntityFactory
    {
        public Entity Create(Vector2 position);
    }
}