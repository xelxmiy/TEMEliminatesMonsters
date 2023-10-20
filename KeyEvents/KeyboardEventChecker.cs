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

        public KeyboardEventChecker() 
        {
            Updateables.IUpdateable m = this; m.AddSelfToUpdateables();
        }

        public KeyboardState GetState()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
            return _currentKeyState;
        }

        public bool IsPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key);
        }

        public bool HasBeenPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }
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
