using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteration : MonoBehaviour, IInteraction
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer sprite;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ButtonPress()
    {
        boxCollider2D.enabled = false;
        sprite.sprite = null;
    }
}
