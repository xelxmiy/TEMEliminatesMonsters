using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TEMEliminatesMonsters.KeyEvents
{
    public static class KeyboardEventManager
    {

        private static readonly Dictionary<Keys, KeyEvent> KeyDownEvents;

        /// <summary>
        /// Creates a Keyboard Events Manager and initializes the keyboard events
        /// </summary>
        static KeyboardEventManager() 
        {
            KeyDownEvents = new Dictionary<Keys, KeyEvent>();
            CreateKeyEvents();
        }

        /// <summary>
        /// Creates Key events for each of the possible keys in the Keys Enum
        /// </summary>
        private static void CreateKeyEvents()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                KeyDownEvents.Add(key, new());
            }
        }

        /// <summary>
        /// returns a refrence to the _event corresponding to the provided key
        /// </summary>
        /// <param name="key">Key corresponding to a KeyEvent</param>
        /// <returns>a refrence to the _event corresponding to the provided key</returns>
        public static ref Action GetEvent(Keys key) 
        {
            KeyDownEvents.TryGetValue(key, out KeyEvent keyEvent);
            return ref keyEvent.GetEvent();
        }
    }
}



