using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMEliminatesMonsters.KeyEvents
{
    public static class KeyboardEventManager
    {

        public static Dictionary<Keys, KeyEvent> KeyboardEvents;

        static KeyboardEventManager() 
        {
            KeyboardEvents = new Dictionary<Keys, KeyEvent>();
            CreateKeyEvents();
        }

        private static void CreateKeyEvents()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                KeyboardEvents.Add(key, new());
            }
        }

        public static ref Action GetEvent(Keys key) 
        {
            KeyboardEvents.TryGetValue(key, out KeyEvent keyEvent);
            return ref keyEvent.GetEvent();
        }
    }
}



