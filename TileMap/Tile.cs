using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.TileMap
{
    public class Tile
    {
        public Texture2D _texture;

        private int _width;

        private int _height;

        public Vector2 _position;

        private int? _id;

        public Tile(Texture2D texture, Vector2 position, int width, int height, int? ID = null)
        {
            _texture = texture;
            _width = width;
            _height = height;
            _position = position;
            this._id = ID;
        }

        public Tile(Texture2D texture, Vector2 position, int? ID = null) : this(texture, position, texture.Width, texture.Height, ID) { }

        public Tile(Texture2D texture, Vector2 position, int size, int? ID = null) : this(texture, position, size, size, ID) { }

        public void Render(SpriteBatch spriteBatch)
        {
            _texture = TEM.Instance._zombie;
            if (_texture == null) 
            {
                throw new NullReferenceException($"{this} Encounterd Issue: Texture is null!");
            }
            spriteBatch.Draw(_texture, _position, color: default);
        }

        public override string ToString() 
        {
            return "Tile" + _id ;
        }

    }
}
