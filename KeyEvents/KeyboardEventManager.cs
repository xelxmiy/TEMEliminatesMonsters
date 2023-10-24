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

        /// <summary>
        /// Creates a Keyboard Events Manager and initializes the keyboard events
        /// </summary>
        static KeyboardEventManager() 
        {
            KeyboardEvents = new Dictionary<Keys, KeyEvent>();
            CreateKeyEvents();
        }

        /// <summary>
        /// Creates Key events for each of the possible keys in the Keys Enum
        /// </summary>
        private static void CreateKeyEvents()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                KeyboardEvents.Add(key, new());
            }
        }

        /// <summary>
        /// returns a refrence to the event corresponding to the provided key
        /// </summary>
        /// <param name="key">Key corresponding to a KeyEvent</param>
        /// <returns>a refrence to the event corresponding to the provided key</returns>
        public static ref Action GetEvent(Keys key) 
        {
            KeyboardEvents.TryGetValue(key, out KeyEvent keyEvent);
            return ref keyEvent.GetEvent();
        }
    }
}



