using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;

namespace TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners;
internal interface IEntitySpawner
{
	abstract Vector2 Position { get; }

	abstract IEntityFactory Factory { get; }
}
