using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {


    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter Interact called");
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject=kitchenObject;

    }

    public void ClearKitchenObject() {
        kitchenObject=null;

    }

    public bool HasKitchenObject() {
        return kitchenObject!=null;
    }

    public Transform GetObjectSpawnPoint() {
        return counterTopPoint;
    }

    public virtual void InteractAlternate() {
    }
}