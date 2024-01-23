using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using TEMEliminatesMonsters.Src.FileLoading;

namespace TEMEliminatesMonsters.Src.Map.Tiles;

public abstract class Tile
{
	public GameTexture TileTexture;

	public Vector2 _position;

	public readonly int? _id;

	public int _width, _height;

	/// <summary>
	/// initializes this tile
	/// </summary>
	/// <param name="gameTexture">TileTexture of this tile</param>
	/// <param name="position">this tile's position</param>
	/// <param name="ID">this tile's ID</param>
	private Tile (GameTexture gameTexture, Vector2 position, int? ID = null)
	{
		ID ??= _id;
		_id = ID;
		TileTexture = gameTexture;
		if (TileTexture.Texture != null)
		{
			_width = TileTexture.Texture.Width;
			_height = TileTexture.Texture.Height;
			if (_width != _height)
			{
				throw new ArgumentException($"Tile width must be equal to length! TILE: {ID}");
			}
		}
		_position = position;
	}

	public Tile (GameTexture texture, int x, int y, int? ID = null)
		: this(texture, new(x * TileMap.TileSize, y * TileMap.TileSize), ID) { }

	/// <summary>
	/// draws this tile to the screen
	/// </summary>
	/// <param name="spriteBatch">the SpriteBatch responsible for drawing</param>
	public void Render (SpriteBatch spriteBatch)
	{
		if (TileTexture.Texture == null)
		{
			return;
		}

		spriteBatch.Draw(
			TileTexture.Texture, _position * TileTexture.ScaleFactor
			, null, Color.White, 0f,
			new(0, 0),
			new Vector2(TileTexture.ScaleFactor*2), 
			SpriteEffects.None, 0f);
	}

	/// <summary>
	/// returns the Tile's Id
	/// </summary>
	/// <returns>returns the tile's Id</returns>
	public override string ToString ()
	{
		if (_id == null)
			return $"Tile ID not set! Falback: {base.ToString()}";
		return $"Tile {_id} has texture {TileTexture.Texture?.Name} and is a {GetType()?.Name}";
	}
}
