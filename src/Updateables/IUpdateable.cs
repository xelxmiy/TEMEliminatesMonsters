using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.src.Updateables
{
    public interface IUpdateable
    {
        /// <summary>
        /// Runs every frame, updates objects
        /// </summary>
        /// <param name="gameTime">Game uptime</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// adds this objects to the UpdateableManager so it can be updated
        /// </summary>
        public void AddSelfToUpdateables()
        {
            UpdateableManager.Updateables.Add(this);
        }
    }
}
