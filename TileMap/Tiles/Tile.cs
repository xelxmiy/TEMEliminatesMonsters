using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Diagnostics;

namespace TEMEliminatesMonsters.TileMap.Tiles
{
    public abstract class Tile
    {
        public static readonly float _GlobalTileSizeModifier = 1f;

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
        public Tile(Texture2D texture, Vector2 position, int? ID = null)
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
                    throw new Exception($"Tile width must be equal to length! TILE: {ID}");
                }
            }
            _position = position;
        }
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
            spriteBatch.Draw(_texture, _position * _GlobalTileSizeModifier, null, Color.White, 0f, new(0, 0), new Vector2(_GlobalTileSizeModifier), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// returns the Tile's Id
        /// </summary>
        /// <returns>returns the tile's Id</returns>
        public override string ToString()
        {
            if (_id == null)
                return $"Tile ID not set! Falback: {base.ToString()}";
            return "Tile" + _id;
        }
    }
}
