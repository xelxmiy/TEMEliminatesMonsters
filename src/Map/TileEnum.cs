using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Map;

//this enum is so i can load Maps from a file for set maps, and later so i can use it to randomly generate worlds.
//there's probably a better way to do this as i have to manually add textures to this and name the files the same thing every time
public enum TileTexture : uint
{
	Metal_TopLeft = 0,
	Metal_TopMiddle = 1,
	Metal_TopRight = 2,

	Metal_MiddleLeft = 3,
	Metal_MiddleMiddle = 4,
	Metal_MiddleRight = 5,

	Metal_BottomLeft = 6,
	Metal_BottomMiddle = 7,
	Metal_BottomRight = 8,


	Metal_Blocked_TopLeft = 9,
	Metal_Blocked_TopMiddle = 10,
	Metal_Blocked_TopRight = 11,

	Metal_Blocked_MiddleLeft = 12,
	Metal_Blocked_MiddleMiddle = 13,
	Metal_Blocked_MiddleRight = 14,

	Metal_Blocked_BottomLeft = 15,
	Metal_Blocked_BottomMiddle = 16,
	Metal_Blocked_BottomRight = 17,
}
