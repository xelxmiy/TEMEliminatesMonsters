using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
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

        public Tile[][,] _tileGrid;

        /// <summary>
        /// creates a new tilemap without a set tile
        /// </summary>
        /// <param name="length">length of tilemap</param>
        /// <param name="width">width of tilemap</param>
        public TileMap(int length, int width) : this(null, 1, length, width) { }
        /// <summary>
        /// Creates a new Tilemap from a Tile Grid
        /// </summary>
        /// <param name="tileMap"></param>
        public TileMap(Tile[][,] tileMap) => _tileGrid = tileMap;

        /// <summary>
        /// Creates a new Tilemap
        /// </summary>
        /// <param name="singleTileTexture">Tile this tilemap is made of</param>
        /// <param name="length">length of tilemap</param>
        /// <param name="width">width of tilemap</param>
        public TileMap(Texture2D singleTileTexture, int layers, int length, int width)
        {
            if (!(layers > 0))
            {
                throw new ArgumentException($"{nameof(layers)} Cannot be less than or equal to 0! it's currently {layers}");
            }
            _tileGrid = new Tile[layers][,];
            for (int i = 0; i < layers; i++)
            {
                _tileGrid[i] = new Tile[length, width];
            }
            InitializeTileMap(singleTileTexture);
        }

        /// <summary>
        /// Initialized the tiles in the tilemap
        /// </summary>
        /// <param name="tiles">the tile this tilemap is composed of</param>
        private void InitializeTileMap(Texture2D tiles = null)
        {
            tiles ??= TEM.Instance.Tiles["Metal-1-1"];
            //intialize all tiles
            int id = 0;            
            foreach (Tile[,] allLayers in _tileGrid) 
            {
                for (int l = 0; l < allLayers.GetLength(0); l++)
                {
                    for (int w = 0; w < allLayers.GetLength(1); w++)
                    {
                        allLayers[l, w] = new(null, new(l * 32, w * 32), id);
                        id++;
                    }
                }
            }
            //initialize base layer
            Tile[,] baseLayer = _tileGrid[0];
            for (int l = 0; l < baseLayer.GetLength(0); l++)
            {
                for (int w = 0; w < baseLayer.GetLength(1); w++)
                {
                    baseLayer[l, w] = new(tiles, new(l * 32, w * 32), id);
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
            foreach (Tile[,] tileLayer in _tileGrid)
            {
                foreach (Tile tile in tileLayer)
                {
                    tile.Render(spriteBatch);
                }
            }
        }
    }
}
