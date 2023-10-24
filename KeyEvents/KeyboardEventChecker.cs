using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using TEMEliminatesMonsters.Updateables;

namespace TEMEliminatesMonsters.KeyEvents
{
    public class KeyboardEventChecker : Updateables.IUpdateable
    {
        KeyboardState _currentKeyState, _previousKeyState;

        /// <summary>
        /// Creates a KeyboardEventChecker
        /// </summary>
        public KeyboardEventChecker() 
        {
            (this as Updateables.IUpdateable).AddSelfToUpdateables();
        }

        /// <summary>
        /// returns the current keyboard state and updates the previous and current key states
        /// </summary>
        /// <returns>The current keyboard state</returns>
        public KeyboardState GetState()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
            return _currentKeyState;
        }

        /// <summary>
        /// returns if the provided key is pressed
        /// </summary>
        /// <param name="key">Key to check against</param>
        /// <returns>If the provided key is pressed</returns>
        public bool IsPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key);
        }

        /// <summary>
        /// returns if the key was pressed this frame
        /// </summary>
        /// <param name="key">Key to check against</param>
        /// <returns>if the key was pressed this frame</returns>
        public bool HasBeenPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }
        /// <summary>
        /// Checks against all keys to see which keys have been pressed this frame and invokes the corresponding events
        /// </summary>
        /// <param name="gameTime">The current gametime</param>
        public void Update(GameTime gameTime)
        {
            GetState();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (HasBeenPressed(key))
                {
                    KeyboardEventManager.GetEvent(key)?.Invoke();
                    Debug.WriteLine($"{key} event Invoked!");
                }
            }
        }
    }
}
