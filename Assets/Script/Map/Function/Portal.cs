using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Portal : MonoBehaviour, IInteraction
{

    [Header("传送所去往的场景")]
    [SerializeField]
    private SceneAsset goToScene;
    [SerializeField]
    private Transform transferTarget;
    [SerializeField]
    private Vector3 transferPosition;
    private BoxCollider2D collider2D;
    private Player player;


    void Awake()
    {
        collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<InteractionButton>() != null)
            player = collision.GetComponent<InteractionButton>().player;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractionButton>() != null)
            player = null;
    }

    public void ButtonPress()
    {
        if (goToScene != null)
            TransferToNewScene();
        else
        {
            player.transform.position = TransferPosition();
        }
    }

    private void TransferToNewScene()
    {
        SceneLoadManager.instance.LoadNewScene(goToScene.key, TransferPosition());
    }

    private Vector3 TransferPosition()
    {
        if (transferTarget != null)
            return transferTarget.position;
        else
            return transferPosition;
    }
}
