using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Dacodelaac.Core;

public class GameInput : BaseMono
{
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnDashAction; 
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public override void ListenEvents()
    {
        base.ListenEvents();
        playerInputActions.Player.Interact.performed += InteractOnperformed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnperformed;
        playerInputActions.Player.Dash.performed += DashOnperformed;
    }

    public override void StopListenEvents()
    {
        base.StopListenEvents();
        playerInputActions.Player.Interact.performed -= InteractOnperformed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternateOnperformed;
        playerInputActions.Player.Dash.performed -= DashOnperformed;
    }

    private void DashOnperformed(InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternateOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        
        return inputVector;
    }

    public void OnClickInteractButton()
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public void OnClickInteractAlternateButton()
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    
    public void OnClickDashButton()
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }
}
