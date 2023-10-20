using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.Updateables
{
    internal class UpdateableManager
    {
        public static List<IUpdateable> Updateables { get; set; } = new();

        public static void UpdateAll(GameTime gameTime) 
        {
            foreach (IUpdateable updateable in Updateables) 
            {
                updateable?.Update(gameTime);
            }
        }
    }
}