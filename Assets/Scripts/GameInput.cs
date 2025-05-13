using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private PlayerInputControl playerInputControl;

    public event EventHandler OnInteract;

    public event EventHandler OnInteractAlternate;

    public event EventHandler OnPause;

    void Awake()
    {
        if(Instance!=null) {
            Debug.LogError($"A game input is already assigned! Conflicting instance: {gameObject.name}");
        }
        Instance=this;

        playerInputControl = new PlayerInputControl();
        playerInputControl.Enable();

        playerInputControl.Player.Interact.performed += Interact_performed;
        playerInputControl.Player.InteractAlternate.performed+=InteractAlternate_performed;
        playerInputControl.Player.Pause.performed+=Pause_performed;


    }

    private void OnDestroy() {
        playerInputControl.Player.Interact.performed-=Interact_performed;
        playerInputControl.Player.InteractAlternate.performed-=InteractAlternate_performed;
        playerInputControl.Player.Pause.performed-=Pause_performed;
        playerInputControl.Dispose();

    }
    public void Pause_performed() {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext context) {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context) {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 ReadMovementNormalized() {
      
        Vector2 InputVector = playerInputControl.Player.Move.ReadValue<Vector2>();

        return InputVector;
    }
    
    

}
