using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider2D collider2D;
    private PlatformEffector2D effector2D;
    private bool playerOnPlatform;
    private void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        effector2D = GetComponent<PlatformEffector2D>();
        playerOnPlatform = false;
    }

    private void Update()
    {
        if (playerOnPlatform)
        {
            if (Keyboard.current.sKey.wasPressedThisFrame || PlayerManager.instance.player.playerController.inputActions.PlayingGame.DownPlatform.WasPressedThisFrame())
            {
                collider2D.enabled = false;
                //effector2D.rotationalOffset = 180;
                StopAllCoroutines();
                StartCoroutine(EnableCollder());
                Debug.LogError("¿ªÊ¼ÏÂÂä");

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerOnPlatform = true;
    }


    private IEnumerator EnableCollder()
    {
        yield return new WaitForSeconds(.5f);
        collider2D.enabled = true;

        //effector2D.rotationalOffset = 0;
    }
}
