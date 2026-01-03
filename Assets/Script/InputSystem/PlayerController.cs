using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public struct InputActionBaseInfo
{
    public bool isPressed;
    public bool isReleased;
    public bool isPressing;
    public float valueFloat;
    public Vector2 valueVector2;
    public Vector3 valueVector3;
}


public class PlayerController : MonoBehaviour
{

    private PlayerControl inputActions;



    public InputActionBaseInfo mouse0;
    public InputActionBaseInfo mouse1;
    public InputActionBaseInfo jump;
    public InputActionBaseInfo dash;
    public InputActionBaseInfo inputX;
    public InputActionBaseInfo skillFreezeTime;
    public InputActionBaseInfo changeThrowSword;
    public InputActionBaseInfo skillTransfer;

    private void Awake()
    {
        if (inputActions == null)
            inputActions = new PlayerControl();
    }

    void Update()
    {
        InitInputActionInfo<Null>(ref mouse0, inputActions.PlayingGame.Attack);
        InitInputActionInfo<Null>(ref mouse1, inputActions.PlayingGame.Mouse1);
        InitInputActionInfo<Null>(ref jump, inputActions.PlayingGame.Jump);
        InitInputActionInfo<Null>(ref dash, inputActions.PlayingGame.Dash);
        InitInputActionInfo<float>(ref inputX, inputActions.PlayingGame.Move);
        InitInputActionInfo<Null>(ref skillFreezeTime, inputActions.PlayingGame.FreezeTime);
        InitInputActionInfo<Null>(ref skillTransfer, inputActions.PlayingGame.Transfer);

    }

    private void OnEnable()
    {

        inputActions.Enable();
    }
    private void OnDisable()
    {
        if (SceneStateManager.IsApplicationQuitting)
            return;
        inputActions.Disable();
    }

    private void InitInputActionInfo<T>(ref InputActionBaseInfo inputAction, InputAction input)
    {
        inputAction.isPressed = input.WasPressedThisFrame();
        inputAction.isPressing = input.IsPressed();
        inputAction.isReleased = input.WasReleasedThisFrame();
        if (typeof(T) == typeof(float))
            inputAction.valueFloat = input.ReadValue<float>();
        else if (typeof(T) == typeof(Vector2))
            inputAction.valueVector2 = input.ReadValue<Vector2>();
        else if (typeof(T) == typeof(Vector3))
            inputAction.valueVector3 = input.ReadValue<Vector3>();
    }
}
