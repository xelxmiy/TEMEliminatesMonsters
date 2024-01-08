using System;

namespace TEMEliminatesMonsters.src.KeyEvents;

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
