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
	public IEnumerable<Texture2D> LoadAllEnemies ()
	{
		return LoadAllObjectsOfKind(EnemyPath);
	}
	public IEnumerable<Texture2D> LoadAllIcons ()
	{
		return LoadAllObjectsOfKind(IconPath);
	}

	public IEnumerable<Texture2D> LoadAllItems ()
	{
		return LoadAllObjectsOfKind(ItemPath);
	}

	public IEnumerable<GameTexture> LoadAllTiles ()
	{
		foreach (Texture2D texture in LoadAllObjectsOfKind(TilePath))
		{
			yield return new GameTexture(texture, TileMap.TileSize / texture.Width);
		}
	}
	#endregion

	private IEnumerable<Texture2D> LoadAllObjectsOfKind (string path)
	{
		foreach (string file in Directory.GetFiles(ContentPathStart + path).Select(Path.GetFileNameWithoutExtension))
		{
			string filePath = path + file;
			Texture2D texture = _content.Load<Texture2D>(filePath);

			Debug.WriteLine($"{file}");

			yield return texture;
		}
	}

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
