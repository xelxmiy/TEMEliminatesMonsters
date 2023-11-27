using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems
{
    public class GenericDrawSystem : DrawSystem
    {
        public GenericDrawSystem(ref SpriteBatch spirteBatch, ref Vector2 position, ref Texture2D texture) 
        {
            _spriteBatch = spirteBatch;
            _position = position;
        }

        public Vector2 _position;

        private Texture2D _texture;

        private SpriteBatch _spriteBatch;

        public override void Draw(GameTime gameTime) 
        {
            _spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
