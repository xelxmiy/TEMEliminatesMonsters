using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace TEMEliminatesMonsters.TileMap
{
    public class Tile
    {
        public static readonly float _GlobalTileSizeModifier = 2f;

        public Texture2D _texture;

        public Vector2 _position;

        private int? _id;
        /// <summary>
        /// initializes this tile
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="ID"></param>
        public Tile(Texture2D texture, Vector2 position, int? ID = null)
        {
            _texture = texture;
            _position = position;
            _id = ID;
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
            spriteBatch.Draw(_texture, _position*_GlobalTileSizeModifier, null, Color.White, 0f, new(0, 0), new Vector2(_GlobalTileSizeModifier), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// returns the Tile's Id
        /// </summary>
        /// <returns>returns the tile's Id</returns>
        public override string ToString() 
        {
            return "Tile" + _id ;
        }

    }
}
