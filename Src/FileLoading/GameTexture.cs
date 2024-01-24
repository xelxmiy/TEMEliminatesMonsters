using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.Src.FileLoading;
//maybe this should be a struct? 
public sealed record GameTexture
{
	/// <summary>
	/// creats a new GameTexture
	/// </summary>
	/// <param name="texture">This texture</param>
	/// <param name="scaleFactor">the scale factor when rendering it</param>
	public GameTexture(Texture2D texture, int scaleFactor = 1) 
	{
		Texture = texture;
		ScaleFactor = scaleFactor;
	}

	public Texture2D Texture { get; set; }
	public int ScaleFactor { get; set; }
}
