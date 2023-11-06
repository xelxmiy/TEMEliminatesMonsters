using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.TileMap.Tiles
{
    internal class SolidTile : Tile
    {
        public RectangleF _collider;

        public SolidTile(Texture2D texture, Vector2 position, int? ID = null) : base(texture, position, ID)
        {
            if (texture != null)
            {
                _collider = new(position, new(texture.Width * _GlobalTileSizeModifier, texture.Height * _GlobalTileSizeModifier));
            }
        }
    }
}