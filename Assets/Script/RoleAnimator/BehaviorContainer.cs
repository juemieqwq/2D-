using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorContainer : MonoBehaviour
{
    public enum RoleBehavior
    {
        Idle,
        Walk,
        Run,
        Attack,
        Jump,
        Fall,
        Hit,
        Dead,
        Aim,
        Fire
    }

    public RoleBehavior roleBehaviorName;
    public int roleBehaviorSerialNumber = 1;
    [HideInInspector]
    private List<GameObject> PlayGameObjects;

    public List<GameObject> GetPlayGameObjects()
    {
        if (PlayGameObjects == null)
        {
            PlayGameObjects = new List<GameObject>();
            var Children = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (var Child in Children)
            {
                if (Child == Children[0])
                    continue;
                Child.gameObject.SetActive(false);
                PlayGameObjects.Add(Child.gameObject);
            }
        }
        return PlayGameObjects;
    }

}
