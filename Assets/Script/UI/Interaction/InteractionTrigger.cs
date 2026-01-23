using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public interface IInteraction
{
    public void ButtonPress();

}


[RequireComponent(typeof(BoxCollider2D))]
public class InteractionTrigger : MonoBehaviour
{
    private IInteraction host;

    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        host = GetComponent<IInteraction>();
        gameObject.tag = "CanInteraction";
    }

    public void ButtonPress(InputAction.CallbackContext callbackContext)
    {
        host?.ButtonPress();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var interactionButton = collision.GetComponent<InteractionButton>();
        if (interactionButton != null)
        {
            interactionButton.playerController.inputActions.PlayingGame.Interaction.started += ButtonPress;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var interactionButton = collision.GetComponent<InteractionButton>();
        if (interactionButton != null)
        {
            interactionButton.playerController.inputActions.PlayingGame.Interaction.started -= ButtonPress;
        }
    }
}
