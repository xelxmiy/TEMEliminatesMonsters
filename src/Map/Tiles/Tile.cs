using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TEMEliminatesMonsters.Src.FileLoading;

namespace TEMEliminatesMonsters.Src.Map.Tiles;

public abstract class Tile
{
	public float TileSizeMultiplier = 2f;

	public Texture2D _texture;

	public Vector2 _position;

	public readonly int? _id;

	public int _width, _height;

	/// <summary>
	/// initializes this tile
	/// </summary>
	/// <param name="texture"></param>
	/// <param name="position"></param>
	/// <param name="ID"></param>
	private Tile (GameTexture texture, Vector2 position, int? ID = null)
	{
		ID ??= _id;
		_id = ID;
		_texture = texture.Texture;
		TileSizeMultiplier = texture.ScaleFactor;
		if (_texture != null)
		{
			_width = _texture.Width;
			_height = _texture.Height;
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
		if (_texture == null)
		{
			return;
		}
		spriteBatch.Draw(_texture, _position * TileSizeMultiplier, null, Color.White, 0f, new(0, 0), new Vector2(TileSizeMultiplier), SpriteEffects.None, 0f);
	}

	/// <summary>
	/// returns the Tile's Id
	/// </summary>
	/// <returns>returns the tile's Id</returns>
	public override string ToString ()
	{
		if (_id == null)
			return $"Tile ID not set! Falback: {base.ToString()}";
		return $"Tile {_id} has texture {_texture?.Name} and is a {GetType()?.Name}";
	}
}
