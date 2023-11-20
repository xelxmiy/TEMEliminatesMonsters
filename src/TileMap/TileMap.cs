using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using TEMEliminatesMonsters.src.TileMap.Tiles;

namespace TEMEliminatesMonsters.src.TileMap
{
    public class TileMap
    {

        private Tile[][,] _tileGrid;

        public int GridWidth => _tileGrid[0].GetLength(0);
        public int GridLength => _tileGrid[0].GetLength(1);

        public static readonly int _tileSize = 32;

        /// <summary>
        /// creates a new tilemap without a set tile
        /// </summary>
        /// <param name="width">width of tilemap</param>
        /// <param name="width">width of tilemap</param>
        public TileMap(int width, int height) : this(null, 1, width, height) { }
        /// <summary>
        /// Creates a new Tilemap from a Tile Grid
        /// </summary>
        /// <param name="tileMap"></param>
        public TileMap(Tile[][,] tileMap) => _tileGrid = tileMap;

        /// <summary>
        /// Creates a new Tilemap
        /// </summary>
        /// <param name="defaultTexture">Tile this tilemap is made of</param>
        /// <param name="length">width of tilemap</param>
        /// <param name="width">width of tilemap</param>
        public TileMap(Texture2D defaultTexture, int layers, int width, int height)
        {
            if (!(layers > 0))
            {
                throw new ArgumentException($"{nameof(layers)} Cannot be less than or equal to 0! it's currently {layers}");
            }
            _tileGrid = new Tile[layers][,];
            for (int i = 0; i < layers; i++)
            {
                _tileGrid[i] = new Tile[width, height];
            }
            InitializeTileMap(defaultTexture);
        }

        /// <summary>
        /// Initialized the defaultTexture in the tilemap
        /// </summary>
        /// <param name="defaultTexture">the tile this tilemap is composed of</param>
        private void InitializeTileMap(Texture2D defaultTexture = null)
        {
            defaultTexture ??= TEM.Instance.Tiles[$"{TileTexture.Metal_MiddleMiddle}"];
            //initialize base layer
            Tile[,] baseLayer = GetTileLayer(0);
            for (int w = 0; w < baseLayer.GetLength(0); w++)
            {
                for (int h = 0; h < baseLayer.GetLength(1); h++)
                {
                    baseLayer[w, h] = new GroundTile(defaultTexture, w, h, Convert.ToInt32($"000{w:000}{h:000}", 16));
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
        /// <param name="w">w of replaced tile</param>
        /// <param name="h">h of replaced tile</param>
        public void SetTile(Texture2D texture, int layer, int w, int h)
        {
            if (_tileGrid[layer][w, h] != null)
            {
                _tileGrid[layer][w, h]._texture = texture;
            }
            else
            {
                Debug.WriteLine($"TILE AT {layer},  {w} , {h} IS NULL, CANNOT SET TILE");
            }
        }

        public void AddTile(Tile tile, int layer)
        {
            int tileX = (int)(tile._position.X / _tileSize * Tile._tileSizeMultiplier);
            int tileY = (int)(tile._position.Y / _tileSize * Tile._tileSizeMultiplier);
            _tileGrid[layer][tileX, tileY] = tile;
        }
        /// <summary>
        /// Replaces a tile in the tilegrid
        /// </summary>
        /// <param name="tile">Tile replacement</param>
        /// <param name="layer">layer of replaced tile</param>
        /// <param name="w">w of replaced tile</param>
        /// <param name="h">h of replaced tile</param>
        public void SetTile(Tile tile, int layer, int w, int h)
        {
            _tileGrid[layer][w, h] = tile;
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
                var tSize = _tileSize * Tile._tileSizeMultiplier;
                //calculates all defaultTexture in frame, much faster than culling not in frame defaultTexture for large (1000*1000) size boards
                for (int x = (int)Math.Floor(cameraBounds.X / tSize); x <= (int)Math.Floor((cameraBounds.Width + cameraBounds.X) / tSize); x++)
                {
                    for (int y = (int)Math.Floor(cameraBounds.Y / tSize); y <= (int)Math.Floor((cameraBounds.Height + cameraBounds.Y) / tSize); y++)
                    {
                        if (x >= 0 && x <= tileLayer.GetLength(0) - 1 && y >= 0 && y <= tileLayer.GetLength(1) - 1)
                        {
                            tileLayer[x, y]?.Render(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}