using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMEliminatesMonsters.src.Entities.ResourceNodes.Spawners;

namespace TEMEliminatesMonsters.src.Entities.Resource_Nodes.Spawners.Concrete;

internal class HuskSpawner : EntityUpdateSystem
{
    HuskFactory _factory;

    public HuskSpawner(AspectBuilder aspectBuilder, HuskFactory factory) : base(aspectBuilder)
    {
        _factory = factory;
    }
    public override void Initialize(IComponentMapperService mapperService) 
    {
    
    }

    public override void Update(GameTime gameTime) 
    {
    
    }
}
