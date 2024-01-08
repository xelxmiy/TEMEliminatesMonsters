using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Systems.EnemySystems.Husk;

namespace TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners;

public class HuskFactory : IEntityFactory
{
    private World _world;
    private Texture2D _huskTexture;
    private SpriteBatch _spriteBatch;

    public HuskFactory(World world, Texture2D texture, SpriteBatch spriteBatch)
    {
        _world = world;
        _huskTexture = texture;
        _spriteBatch = spriteBatch;
    }

    public Entity Create(Vector2 position)
    {
        Entity husk = _world.CreateEntity();
        husk.Attach(new Transform2(position));
        husk.Attach(_huskTexture);
        // add systems here
        husk.Attach(new HuskMovementSystem(husk.Get<Transform2>()));
        return husk;
    }
}
