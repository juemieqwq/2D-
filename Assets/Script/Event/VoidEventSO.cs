using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("Event/VoidEvent"))]
public class VoidEventSO : ScriptableObject
{
    [SerializeField]
    private UnityEvent voidEvent;
    public void AddEventListener(UnityAction action)
    {
        voidEvent.AddListener(action);
    }

    public void RemoveEventListener(UnityAction action)
    {
        voidEvent.RemoveListener(action);
    }

    public void Raise()
    {
        voidEvent?.Invoke();
    }
}
