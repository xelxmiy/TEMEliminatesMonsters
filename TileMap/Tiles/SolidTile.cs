using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TEMEliminatesMonsters.TileMap.Tiles
{
    internal class SolidTile : Tile
    {
        public RectangleF _collider;

        public SolidTile(Texture2D texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
        {
            if (texture != null)
            {
                _collider = new(new(x*32,y*32), new(texture.Width * _tileSizeMultiplier, texture.Height * _tileSizeMultiplier));
            }
        }
    }
}