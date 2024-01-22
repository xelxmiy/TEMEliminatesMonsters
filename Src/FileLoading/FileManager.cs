using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TEMEliminatesMonsters.Src.FileLoading;
public static class FileManager
{
	private static AssetLoader s_assetLoader { get; }

	public static Dictionary<string, Texture2D> Enemies { get; } = new();
	public static Dictionary<string, Texture2D> Icons { get; } = new();
	public static Dictionary<string, Texture2D> Items { get; } = new();
	public static Dictionary<string, GameTexture> Tiles { get; } = new();

	static FileManager ()
	{
		s_assetLoader = new AssetLoader(TEM.Instance.Content);
	}

	#region Load Files To Dictionaries

	// this method is ONLY called Evils because it makes the code in the LoadContent line up nicely
	// it is still technically accurate since from the player's POV the monsters are evil.
	// but.. it should really be "LoadEnemiesToDictionary()"
	/// <summary>
	/// Loads All Enemy Textures to enemy Dictionary for fast acess
	/// </summary>
	public static void LoadEvilsToDictionary ()
	{
		foreach (Texture2D texture in s_assetLoader.LoadAllEnemies())
		{
			Enemies.Add(texture.Name, texture);
		}
	}

	/// <summary>
	/// Loads all Icon Textures to icon Dictionary for fast acess
	/// </summary>
	public static void LoadIconsToDictionary ()
	{
		foreach (Texture2D texture in s_assetLoader.LoadAllIcons())
		{
			Icons.Add(texture.Name, texture);
		}
	}

	/// <summary>
	/// Loads all Item Textures to item Dictionary for fast acess
	/// </summary>
	public static void LoadItemsToDictionary ()
	{
		foreach (Texture2D texture in s_assetLoader.LoadAllItems())
		{
			Items.Add(texture.Name, texture);
		}
	}

	/// <summary>
	/// Loads all Tiles Textures to tile Dictionary for fast acess
	/// </summary>
	public static void LoadTilesToDictionary ()
	{
		foreach (GameTexture gameTexture in s_assetLoader.LoadAllTiles())
		{
			Tiles.Add(gameTexture.Texture.Name, gameTexture);
		}
	}

	#endregion
}

