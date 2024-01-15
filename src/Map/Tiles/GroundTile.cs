using Microsoft.Xna.Framework.Graphics;

namespace TEMEliminatesMonsters.Src.Map.Tiles;

public class GroundTile : Tile
{
	public GroundTile (Texture2D texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
	{
	}
}
