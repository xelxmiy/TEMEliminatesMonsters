using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TEMEliminatesMonsters.src.Map.Tiles
{
    public abstract class Tile
    {
        public static readonly float _tileSizeMultiplier = 2f;

        public Texture2D _texture;

        public Vector2 _position;

        public readonly int? _id;

        public int _width, _height;

        /// <summary>
        /// initializes this tile
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="ID"></param>
        private Tile(Texture2D texture, Vector2 position, int? ID = null)
        {
            ID ??= _id;
            _id = ID;
            _texture = texture;
            if (_texture != null)
            {
                _width = _texture.Width;
                _height = _texture.Height;
                if (_width != _height)
                {
                    throw new ArgumentException($"Tile width must be equal to length! TILE: {ID}");
                }
                if (_width != TileMap._tileSize || _width == 33) // TODO: remove the 33 line, it's not supposed to be there i just have to rebuild the files
                {
                    throw new ArgumentException($"Tile width/length must be equal to {TileMap._tileSize}, it's {_width}! Tile: {ID}");
                }
            }
            _position = position;
        }

        public Tile(Texture2D texture, int x, int y, int? ID = null)
            : this(texture, new(x * TileMap._tileSize, y * TileMap._tileSize), ID) { }

        /// <summary>
        /// draws this tile to the screen
        /// </summary>
        /// <param name="spriteBatch">the SpriteBatch responsible for drawing</param>
        public void Render(SpriteBatch spriteBatch)
        {
            if (_texture == null)
            {
                return;
            }
            spriteBatch.Draw(_texture, _position * _tileSizeMultiplier, null, Color.White, 0f, new(0, 0), new Vector2(_tileSizeMultiplier), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// returns the Tile's Id
        /// </summary>
        /// <returns>returns the tile's Id</returns>
        public override string ToString()
        {
            if (_id == null)
                return $"Tile ID not set! Falback: {base.ToString()}";
            return $"Tile {_id} has texture {_texture?.Name} and is a {GetType()?.Name}";
        }
    }
}
