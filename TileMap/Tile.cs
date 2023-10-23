using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace TEMEliminatesMonsters.TileMap
{
    public class Tile
    {
        public Texture2D _texture;

        public Vector2 _position;

        private int? _id;

        public Tile(Texture2D texture, Vector2 position, int? ID = null)
        {
            _texture = texture;
            _position = position;
            _id = ID;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if (_texture == null) 
            {
                throw new NullReferenceException($"{this} Encounterd Issue: Texture is null!");
            }
            spriteBatch.Draw(_texture, _position*3, null, Color.White, 0f, new(0, 0), new Vector2(3), SpriteEffects.None, 0f);
        }

        public override string ToString() 
        {
            return "Tile" + _id ;
        }

    }
}
