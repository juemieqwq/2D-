using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SavePoint : MonoBehaviour, IInteraction
{
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private GameObject light;

    [Header("¹ã²¥")]
    [SerializeField]
    private VoidEventSO saveDataEventSO;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void ButtonPress()
    {
        boxCollider2D.enabled = false;
        light.SetActive(true);
        saveDataEventSO.Raise();
    }
}
