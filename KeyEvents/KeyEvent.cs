using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.KeyEvents
{
    public class KeyEvent
    {
        private event Action Event;

        /// <summary>
        /// invokes the encapsulated _event
        /// </summary>
        public void Invoke()
        {
            Event?.Invoke();
        }

        /// <summary>
        /// returns a refrence to the encapsulated _event
        /// </summary>
        /// <returns></returns>
        public ref Action GetEvent() 
        {
            return ref Event;
        }
    }
}
