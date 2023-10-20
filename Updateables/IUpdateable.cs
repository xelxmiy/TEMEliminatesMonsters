using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.Updateables
{
    public interface IUpdateable
    {
        public abstract void Update(GameTime gameTime);

        public void AddSelfToUpdateables() 
        {
            UpdateableManager.Updateables.Add(this);
        }
    }
}
