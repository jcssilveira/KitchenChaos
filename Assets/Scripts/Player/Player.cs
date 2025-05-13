using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private float moveSpeed = 20f;
    //[SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform objectSpawnPoint;

    KitchenObject kitchenObject;

    public event EventHandler<EventArgs> OnObjectPickup; //used for audio effects
    public event EventHandler<EventArgs> OnObjectDrop;



    public event EventHandler<InteractEventArgs> OnSelectedCounterChanged;

    public class InteractEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }


    public static Player Instance { get; private set; } 

    private bool isMoving = false;
 
    private BaseCounter selectedCounter;

    private Vector3 interactDirection;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError($"A player is already assigned! Conflicting instance: {gameObject.name}");
        }
        Instance = this;
        
    }


    private void Start() {
        GameInput.Instance.OnInteract+=GameInput_OnInteract;
        GameInput.Instance.OnInteractAlternate+=GameInput_OnInteractAlternate;
    }

    private void GameInput_OnInteractAlternate(object sender, EventArgs e) {
        if(selectedCounter!=null) {
            selectedCounter.InteractAlternate();
        }
    }

    private void GameInput_OnInteract(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        if(!GameManager.Instance.IsGamePlaying()) {
            return;
        }

        ReadInput();

        LookForInteraction();

        
    }
    private void LookForInteraction() {
        Vector2 InputVector = GameInput.Instance.ReadMovementNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);
        selectedCounter=null; 
        if (moveDir != Vector3.zero) {
            interactDirection = moveDir;
            RotateCharacter(moveDir); //deveria melhorar isso aqui pra rotacionar só em readInput
        }

        if(Physics.Raycast(transform.position, interactDirection, out RaycastHit hit, 1.5f, countersLayerMask)) {
            if(hit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                SetSelectedCounter(baseCounter);
                return;
            }
        }
        SetSelectedCounter(null);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new InteractEventArgs { selectedCounter=selectedCounter });
    }

    private void ReadInput() {
        Vector2 InputVector = GameInput.Instance.ReadMovementNormalized();

        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);

        float moveDistance = moveSpeed*Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        Vector3 capsuleBottom = transform.position-Vector3.up*(playerHeight/2);
        Vector3 capsuleTop = transform.position+Vector3.up*(playerHeight/2);

        moveDir=CheckHitOptions(capsuleBottom, capsuleTop, playerRadius, moveDir, moveDistance);

        transform.position+=moveDir*Time.deltaTime*moveSpeed;

        isMoving=moveDir!=Vector3.zero;


        RotateCharacter(moveDir);

    }

    private void RotateCharacter(Vector3 moveDir) {
        float rotationSpeed = 20f;
        if(moveDir!=Vector3.zero) {
            transform.forward=Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotationSpeed);
        }
    }

    private Vector3 CheckHitOptions(Vector3 capsuleBottom, Vector3 capsuleTop, float playerRadius, Vector3 moveDir, float moveDistance) {
        List<Vector3> moveOptions = new List<Vector3>()
        {
            new Vector3(moveDir.x, moveDir.y, moveDir.z),
            new Vector3(moveDir.x, moveDir.y, 0),  // z = 0
            new Vector3(0, moveDir.y, moveDir.z),  // x = 0
            new Vector3(0, moveDir.y, 0),          // x = 0 e z = 0
            new Vector3(0, 0, 0)
        };
        foreach(Vector3 option in moveOptions) {
            moveDir=option.normalized;

            bool canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDir, moveDistance);

            if(canMove) {
                return moveDir;
            }

        }

        return moveDir; //not needed
    }

    public bool IsMoving() {
        return isMoving;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject=kitchenObject;
        if(kitchenObject!=null) {
           OnObjectDrop?.Invoke(this, EventArgs.Empty);
        }

    }

    public void ClearKitchenObject() {
        kitchenObject=null;
        OnObjectPickup?.Invoke(this, EventArgs.Empty);

    }

    public bool HasKitchenObject() {
        return kitchenObject!=null;
    }

    public Transform GetObjectSpawnPoint() {
        return objectSpawnPoint;
    }
}
