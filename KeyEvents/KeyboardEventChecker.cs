using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace TEMEliminatesMonsters.KeyEvents
{
    static class KeyboardEventChecker
    {
        static KeyboardState _currentKeyState, _previousKeyState;

        public static KeyboardState GetState()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
            return _currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key);
        }

        public static bool HasBeenPressed(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }
        public static void Update()
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
