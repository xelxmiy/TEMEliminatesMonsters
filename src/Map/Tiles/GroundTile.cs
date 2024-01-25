using Microsoft.Xna.Framework.Graphics;
using TEMEliminatesMonsters.Src.FileLoading;

namespace TEMEliminatesMonsters.Src.Map.Tiles;


//Todo: make this do *anything*
public class GroundTile : Tile
{

	/// <summary>
	/// Creates a new Ground Tile
	/// </summary>
	/// <param name="texture">texture of this tile</param>
	/// <param name="x">x position of this tiel</param>
	/// <param name="y">y position of this tile</param>
	/// <param name="ID">this tile's ID</param>
	public GroundTile (GameTexture texture, int x, int y, int? ID = null) : base(texture, x, y, ID)
	{
	}
}
