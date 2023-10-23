using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.TileMap
{
    public class TileMap
    {

        public Tile[,] _tileMap;

        /// <summary>
        /// creates a new tilemap without a set tile
        /// </summary>
        /// <param name="length">length of tilemap</param>
        /// <param name="width">width of tilemap</param>
        public TileMap(int length, int width) : this(null, length, width) { }
        /// <summary>
        /// Creates a new Tilemap from a Tile Grid
        /// </summary>
        /// <param name="tileMap"></param>
        public TileMap(Tile[,] tileMap) => _tileMap = tileMap;

        /// <summary>
        /// Creates a new Tilemap
        /// </summary>
        /// <param name="singleTileTexture">Tile this tilemap is made of</param>
        /// <param name="length">length of tilemap</param>
        /// <param name="width">width of tilemap</param>
        public TileMap(Texture2D singleTileTexture, int length, int width) 
        {
            _tileMap = new Tile[length,width];
            InitializeTileMap(singleTileTexture);   
        }
        /// <summary>
        /// Initialized the tiles in the tilemap
        /// </summary>
        /// <param name="tiles">the tile this tilemap is composed of</param>
        private void InitializeTileMap(Texture2D tiles = null) 
        {
            tiles ??= TEM.Instance.Tiles["Metal-1-1"];
            int id = 0;
            for (int l = 0; l < _tileMap.GetLength(0); l++) 
            {
                for (int w = 0; w < _tileMap.GetLength(1); w++) 
                {
                    _tileMap[l, w] = new(tiles, new(l * 32 , w * 32), id);
                    id++;
                }
            }  
        }
        /// <summary>
        /// draws this tilemap to the screen
        /// </summary>
        /// <param name="spriteBatch">the SpriteBatch responsible for drawing</param>
        public void Render(SpriteBatch spriteBatch) 
        {
            foreach (Tile tile in _tileMap) 
            {
                tile.Render(spriteBatch);
            }
        }
    }
}
