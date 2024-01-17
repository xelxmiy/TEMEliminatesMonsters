using Microsoft.Xna.Framework;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;

namespace TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners;
//mandates that Entity Spawners have a factory, as well as a position
internal interface IEntitySpawner
{
	abstract Vector2 Position { get; }

	abstract IEntityFactory Factory { get; }
}
