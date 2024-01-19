using Microsoft.Xna.Framework.Graphics;
using TEMEliminatesMonsters.Src.FileLoading;

namespace TEMEliminatesMonsters.Src.Map.Tiles;

public class GroundTile : Tile
{
	public GroundTile (GameTexture texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
	{
	}
}
