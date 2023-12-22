using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Collisions;
using TEMEliminatesMonsters.src.Updateables;

namespace TEMEliminatesMonsters.src.Map.Tiles
{
    abstract class TrapTile : Tile, ICollisionActor
    {
        public TrapTile(Texture2D texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
        {
            if (texture != null)
            {
                _bounds = new RectangleF(new(x * 32, y * 32), new(texture.Width * _tileSizeMultiplier, texture.Height * _tileSizeMultiplier));
            }
        }

        private IShapeF _bounds;

        IShapeF ICollisionActor.Bounds => _bounds;

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            OnTrapEnter(collisionInfo.Other);
        }
        public abstract void OnTrapEnter(ICollisionActor @object);
    }
}