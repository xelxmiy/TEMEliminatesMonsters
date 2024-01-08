using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Updateables;

internal class UpdateableManager
{
    public static List<IUpdateable> Updateables { get; set; } = new();

    /// <summary>
    /// Runs every frame, updates all Updateable objects
    /// </summary>
    /// <param name="gameTime">Game uptime</param>
    public static void UpdateAll(GameTime gameTime)
    {
        foreach (IUpdateable updateable in Updateables)
        {
            updateable?.Update(gameTime);
        }
    }
}