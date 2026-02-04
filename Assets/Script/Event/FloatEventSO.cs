using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FloatEvent")]
public class FloatEventSO : ScriptableObject
{

    public UnityEvent<float> floatEvent;

    public void AddEventListener(UnityAction<float> action)
    {
        floatEvent.AddListener(action);
    }

    public void RemoveEventListener(UnityAction<float> action)
    {
        floatEvent.RemoveListener(action);
    }

    public void Raise(float value)
    {
        floatEvent?.Invoke(value);
    }
}
