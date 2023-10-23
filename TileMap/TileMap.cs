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

        public TileMap(int length, int width) : this(null, length, width) { }

        public TileMap(Tile[,] tileMap) => _tileMap = tileMap;

        public TileMap(Texture2D singleTileTexture, int length, int width) 
        {
            _tileMap = new Tile[length,width];
            InitializeTileMap(singleTileTexture);   
        }

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

        public void Render(SpriteBatch spriteBatch) 
        {
            foreach (Tile tile in _tileMap) 
            {
                tile.Render(spriteBatch);
            }
        }
    }
}
