using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.TileMap.Tiles
{
    public class GroundTile : Tile
    {
        public GroundTile(Texture2D texture, Vector2 position, int? ID = null) : base(texture, position, ID)
        {
        }
    }
}
