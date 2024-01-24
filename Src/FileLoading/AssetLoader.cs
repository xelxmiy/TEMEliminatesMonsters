using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TEMEliminatesMonsters.Src.Map;

namespace TEMEliminatesMonsters.Src.FileLoading;
internal class AssetLoader
{

	private readonly ContentManager _content;

	/// <summary>
	/// creates a new AssetLoader
	/// </summary>
	/// <param name="content">ContentManager containg the content path</param>
	public AssetLoader (ContentManager content)
	{
		_content = content;
	}

	#region Path Constants
	private static readonly string ContentPathStart = "Content\\";

	private static readonly string EnemyPath = "Enemies\\";

	private static readonly string IconPath = "Icons\\";

	private static readonly string ItemPath = "Items\\";

	private static readonly string TilePath = "Tiles\\";
	#endregion

	#region Individual Kind Loading
	/// <summary>
	/// Loads all the textures for enemies
	/// </summary>
	/// <returns>textures for all enemies</returns>
	public IEnumerable<Texture2D> LoadAllEnemies ()
	{
		return LoadAllObjectsOfKind(EnemyPath);
	}
	/// <summary>
	/// Loads all the textures for icons
	/// </summary>
	/// <returns>textures for all Icons</returns>
	public IEnumerable<Texture2D> LoadAllIcons ()
	{
		return LoadAllObjectsOfKind(IconPath);
	}

	/// <summary>
	/// Loads all the textures for items
	/// </summary>
	/// <returns>Textures of all items</returns>
	public IEnumerable<Texture2D> LoadAllItems ()
	{
		return LoadAllObjectsOfKind(ItemPath);
	}

	/// <summary>
	/// loads textures of all tiles
	/// </summary>
	/// <returns>GameTextures of all tiles</returns>
	public IEnumerable<GameTexture> LoadAllTiles ()
	{
		foreach (Texture2D texture in LoadAllObjectsOfKind(TilePath))
		{
			Debug.WriteLine($"Tile has Scaling Factor of { TileMap.TileSize / texture.Width}");
			yield return new GameTexture(texture, TileMap.TileSize / texture.Width);
		}
	}
	#endregion

	/// <summary>
	/// loads all assets within a directory
	/// </summary>
	/// <param name="path">path to directory</param>
	/// <returns>Textures of all things loaded inside provided path</returns>
	private IEnumerable<Texture2D> LoadAllObjectsOfKind (string path)
	{
		foreach (string file in Directory.GetFiles(ContentPathStart + path).Select(Path.GetFileNameWithoutExtension))
		{
			string filePath = path + file;
			Texture2D texture = _content.Load<Texture2D>(filePath);

			Debug.WriteLine($"Loaded File with Name {file}");

			yield return texture;
		}
	}

	/// <summary>
	/// attempts to load a texture in a specific path
	/// </summary>
	/// <param name="path">the path of the desired texture</param>
	/// <param name="result">the texture located at the provided path</param>
	/// <returns>weather loading was sucessful</returns>
	public bool TryLoadTexture (string path, out Texture2D result)
	{
		try
		{
			result = _content.Load<Texture2D>(path);
			return true;
		}
		catch
		{
			Debug.WriteLine(string.Concat("Could not locate Texture with path ", path, "!"));
			result = null;
			return false;
		}
	}
}
