using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TEMEliminatesMonsters.src.KeyEvents;

public class KeyboardEventChecker : Updateables.IUpdateable
{
    private KeyboardState _currentKeyState, _previousKeyState;

    private readonly List<Keys> _allKeys;

    /// <summary>
    /// Creates a KeyboardEventChecker
    /// </summary>
    public KeyboardEventChecker()
    {
        (this as Updateables.IUpdateable).AddSelfToUpdateables();
        _allKeys = ((Keys[])Enum.GetValues(typeof(Keys))).ToList();

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
        foreach (Keys key in _allKeys)
        {
            if (HasBeenPressed(key))
            {
                KeyboardEventManager.GetEvent(key)?.Invoke();
                Debug.WriteLine($"{key} _event Invoked!");
            }
        }
    }
}
