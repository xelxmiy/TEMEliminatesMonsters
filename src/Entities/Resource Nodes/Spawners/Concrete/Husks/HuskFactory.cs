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
    private readonly World _world;
    private readonly Texture2D _huskTexture;

    public HuskFactory(World world, Texture2D texture)
    {
        _world = world;
        _huskTexture = texture;
    }

    public Entity Create(Vector2 position)
    {
        //Creates a husk entity
        Entity husk = _world.CreateEntity();
        husk.Attach(new Transform2(position));
        husk.Attach(_huskTexture);
        // add systems here
        husk.Attach(new HuskMovementSystem(husk.Get<Transform2>()));
        return husk;
    }
}
