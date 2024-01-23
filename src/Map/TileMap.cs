using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using TEMEliminatesMonsters.Src.Controllers;
using TEMEliminatesMonsters.Src.FileLoading;
using TEMEliminatesMonsters.Src.Map.Tiles;

namespace TEMEliminatesMonsters.Src.Map;

public class TileMap
{

	private Tile[][,] _tileGrid;

	public int GridWidth => _tileGrid[0].GetLength(0);
	public int GridLength => _tileGrid[0].GetLength(1);

	public static readonly int TileSize = 32;

	/// <summary>
	/// creates a new tilemap without a set tile
	/// </summary>
	/// <param name="width">width of tilemap</param>
	/// <param name="width">width of tilemap</param>
	public TileMap (int width, int height) : this(default, 1, width, height) { }
	/// <summary>
	/// Creates a new Tilemap from a Tile Grid
	/// </summary>
	/// <param name="tileMap"></param>
	public TileMap (Tile[][,] tileMap) => _tileGrid = tileMap;

	/// <summary>
	/// Creates a new Tilemap
	/// </summary>
	/// <param name="defaultTexture">Tile this tilemap is made of</param>
	/// <param name="length">width of tilemap</param>
	/// <param name="width">width of tilemap</param>
	public TileMap (GameTexture defaultTexture, int layers, int width, int height)
	{
		if (!(layers > 0))
		{
			throw new ArgumentException($"{nameof(layers)} Cannot be less than or equal to 0! it's currently {layers}");
		}
		_tileGrid = new Tile[layers][,];
		for (int i = 0; i < layers; i++)
		{
			_tileGrid[i] = new Tile[width, height];
		}
		InitializeTileMap(defaultTexture);
	}

	/// <summary>
	/// Initialized the defaultTexture in the tilemap
	/// </summary>
	/// <param name="defaultTexture">the tile this tilemap is composed of</param>
	private void InitializeTileMap (GameTexture defaultTexture = default)
	{
		if (defaultTexture.Equals(default(GameTexture)))
		{
			defaultTexture = FileManager.Tiles["Tiles\\Metal_MiddleMiddle"];
		}
		//initialize base layer
		Tile[,] baseLayer = GetTileLayer(0);
		for (int w = 0; w < baseLayer.GetLength(0); w++)
		{
			for (int h = 0; h < baseLayer.GetLength(1); h++)
			{
				baseLayer[w, h] = new GroundTile(defaultTexture, w, h, Convert.ToInt32($"000{w:000}{h:000}", 16));
			}
		}
	}

	/// <summary>
	/// Returns a layer of the tilemap
	/// </summary>
	/// <param name="layer">Layer number</param>
	/// <returns>A layer of the tilemap</returns>
	/// <exception cref="IndexOutOfRangeException">Occurs on Invalid layers</exception>
	public Tile[,] GetTileLayer (int layer)
	{
		if (layer >= _tileGrid.Length || layer < 0)
		{
			throw new IndexOutOfRangeException($"{nameof(layer)} cannot be less than 0 or greater than {_tileGrid.Length - 1}!");
		}
		return _tileGrid[layer];
	}

	public void AddTile (Tile tile, int layer)
	{
		int tileX = (int)(tile._position.X / TileSize * tile.TileTexture.ScaleFactor);
		int tileY = (int)(tile._position.Y / TileSize * tile.TileTexture.ScaleFactor);
		_tileGrid[layer][tileX, tileY] = tile;
	}
	/// <summary>
	/// Replaces a tile in the tilegrid
	/// </summary>
	/// <param name="tile">Tile replacement</param>
	/// <param name="layer">layer of replaced tile</param>
	/// <param name="width">width of replaced tile</param>
	/// <param name="height">height of replaced tile</param>
	public void SetTile (Tile tile, int layer, int width, int height)
	{
		_tileGrid[layer][width, height] = tile;
	}

	/// <summary>
	/// Draws this tilemap to the screen
	/// </summary>
	/// <param name="spriteBatch">The SpriteBatch responsible for drawing</param>
	public void Render (SpriteBatch spriteBatch)
	{
		foreach (Tile[,] tileLayer in _tileGrid)
		{
			//belive it or not, using 'var' is standard for Monogame projects 
			var cameraBounds = TEM.Instance.Camera.BoundingRectangle;
			var tileSize = TileSize * 2; // TODO: fix this so it's more general
			//calculates all defaultTexture in frame, much faster than culling not in frame textures for large (1000*1000) size boards
			for (int x = (int)Math.Floor(cameraBounds.X / tileSize); x <= (int)Math.Floor((cameraBounds.Width + cameraBounds.X) / tileSize); x++)
			{
				for (int y = (int)Math.Floor(cameraBounds.Y / tileSize); y <= (int)Math.Floor((cameraBounds.Height + cameraBounds.Y) / tileSize); y++)
				{
					if (x >= 0 && x <= tileLayer.GetLength(0) - 1 && y >= 0 && y <= tileLayer.GetLength(1) - 1)
					{
						tileLayer[x, y]?.Render(spriteBatch);
					}
				}
			}
		}
	}
}