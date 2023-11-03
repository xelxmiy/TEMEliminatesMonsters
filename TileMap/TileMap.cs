using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TEMEliminatesMonsters.TileMap
{
    public class TileMap
    {

        private Tile[][,] _tileGrid;

        private const int _tileSize = 32;

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
            tiles ??= TEM.Instance.Tiles[$"{TileTexture.Metal_MiddleMiddle}"]; // 
            //intialize all tiles
            int id = 0;
            foreach (Tile[,] allLayers in _tileGrid)
            {
                for (int l = 0; l < allLayers.GetLength(0); l++)
                {
                    for (int w = 0; w < allLayers.GetLength(1); w++)
                    {
                        allLayers[l, w] = new(null, new(l * _tileSize, w * _tileSize), id);
                        id++;
                    }
                }
            }
            //initialize base layer
            Tile[,] baseLayer = GetTileLayer(0);
            for (int l = 0; l < baseLayer.GetLength(0); l++)
            {
                for (int w = 0; w < baseLayer.GetLength(1); w++)
                {

                    Texture2D tex = TEM.Instance.Tiles[$"{TileTexture.Metal_MiddleMiddle}"];
                    baseLayer[l, w] = new(tex, new(l * _tileSize, w * _tileSize));
                }
            }
        }

        /// <summary>
        /// Returns a layer of the tilemap
        /// </summary>
        /// <param name="layer">Layer number</param>
        /// <returns>A layer of the tilemap</returns>
        /// <exception cref="IndexOutOfRangeException">Occurs on Invalid layers</exception>
        public Tile[,] GetTileLayer(int layer)
        {
            if (layer >= _tileGrid.Length || layer < 0)
            {
                throw new IndexOutOfRangeException($"{nameof(layer)} cannot be less than 0 or greater than {_tileGrid.Length - 1}!");
            }
            return _tileGrid[layer];
        }
        /// <summary>
        /// Replaces the Texture tile in the tilegrid
        /// </summary>
        /// <param name="texture">Texture replacement</param>
        /// <param name="layer">Layer of replaced tile</param>
        /// <param name="x">x of replaced tile</param>
        /// <param name="y">y of replaced tile</param>
        public void SetTile(Texture2D texture, int layer, int x, int y)
        {
            _tileGrid[layer][x, y]._texture = texture;
        }

        /// <summary>
        /// Replaces a tile in the tilegrid
        /// </summary>
        /// <param name="tile">Tile replacement</param>
        /// <param name="layer">layer of replaced tile</param>
        /// <param name="x">x of replaced tile</param>
        /// <param name="y">y of replaced tile</param>
        public void SetTile(Tile tile, int layer, int x, int y)
        {
            _tileGrid[layer][x, y] = tile;
        }

        /// <summary>
        /// Draws this tilemap to the screen
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch responsible for drawing</param>
        public void Render(SpriteBatch spriteBatch)
        {
            foreach (Tile[,] tileLayer in _tileGrid)
            {
                //belive it or not, using 'var' is standard for Monogame projects 
                var cameraBounds = TEM.Instance._camera.BoundingRectangle;
                var tSize = (_tileSize * Tile._GlobalTileSizeModifier);
                //calculates all tiles in frame, much faster than culling not in frame tiles for large (1000*1000) size boards
                for (int x = (int)Math.Floor(cameraBounds.X / tSize); x <= (int)Math.Floor((cameraBounds.Width + cameraBounds.X) / tSize); x++)
                {
                    for (int y = (int)Math.Floor(cameraBounds.Y / tSize); y <= (int)Math.Floor((cameraBounds.Height + cameraBounds.Y) / tSize); y++)
                    {
                        if (x >= 0 && x <= tileLayer.GetLength(0) -1 && y >= 0 && y <= tileLayer.GetLength(1) -1)
                        {
                            
                            tileLayer[x, y].Render(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}