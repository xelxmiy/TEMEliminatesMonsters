using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.EnemySystems.Husk;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners
{
    public class HuskFactory
    {
        public World _world;
        public Texture2D _huskTexture;

        public HuskFactory(ref World world, Texture2D texture)
        {
            _world = world;
            _huskTexture = texture;
        }

        public Entity CreateHusk(Vector2 position)
        {
            Entity husk = _world.CreateEntity();
            husk.Attach(new Transform2(position));
            husk.Attach(_huskTexture);

            // add systems here
            husk.Attach(new HuskMovementSystem(husk.Get<Transform2>()));
            husk.Attach(new GenericDrawSystem(TEM.Instance.SpriteBatch, position, _huskTexture));

            return husk;
        }
    }
}
