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

        public void Invoke()
        {
            @event?.Invoke();
        }

        public ref Action GetEvent() 
        {
            return ref @event;
        }
    }
}
