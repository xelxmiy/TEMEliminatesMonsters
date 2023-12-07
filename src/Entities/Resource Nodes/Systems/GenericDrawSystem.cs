using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems
{
    public class GenericDrawSystem : DrawSystem
    {
        public GenericDrawSystem(SpriteBatch spirteBatch, Vector2 position, Texture2D texture)
        {
            _spriteBatch = spirteBatch;
            _position = position;
            _texture = texture;
        }

        public Vector2 _position;

        private Texture2D _texture;

        private SpriteBatch _spriteBatch;

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_texture, _position, Color.White);
            Debug.WriteLine("Drew Something!");
        }
    }
}
