using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.KeyEvents
{
    public class KeyEvent
    {
        private event Action @event;

        /// <summary>
        /// invokes the encapsulated event
        /// </summary>
        public void Invoke()
        {
            @event?.Invoke();
        }

        /// <summary>
        /// returns a refrence to the encapsulated event
        /// </summary>
        /// <returns></returns>
        public ref Action GetEvent() 
        {
            return ref @event;
        }
    }
}
