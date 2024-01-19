using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TEMEliminatesMonsters.Src.FileLoading;
using TEMEliminatesMonsters.Src.Map;

namespace TEMEliminatesMonsters.Src.FileLoading;
public class AssetLoader
{

	private readonly ContentManager _content;

	public AssetLoader (ContentManager content)
	{
		_content = content;
	}

	public IEnumerable LoadAllTiles () 
	{
		foreach (string file in Directory.GetFiles("Content\\Tiles\\").Select(Path.GetFileNameWithoutExtension))
		{
			string s = "Tiles\\" + file;
			Texture2D texture = _content.Load<Texture2D>(s);;
			Debug.WriteLine($"{file}, {TileMap.TileSize / texture.Width}");
			yield return new GameTexture(texture, TileMap.TileSize / texture.Width);
		}
	}
}
