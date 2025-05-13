using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject: MonoBehaviour {
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {


        if(this.kitchenObjectParent !=null) { // se o objeto atual j� tiver um dono, remova-o do dono
            this.kitchenObjectParent.ClearKitchenObject();

        }


        this.kitchenObjectParent=kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("KitchenObjectParent already has an object");
        }

        kitchenObjectParent.SetKitchenObject(this);
        
        transform.SetParent(kitchenObjectParent.GetObjectSpawnPoint()); //vai mudar a posi��o do pai pra posi��o do novo counter
        transform.localPosition=Vector3.zero; //vai mudar a posi��o do objeto pra 0,0,0 em rela��o ao pai
        

    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    static public KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) { //incerto dessa fun��o
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); 
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plate) {
        plate=null;
        if(this is PlateKitchenObject) {
            plate=(PlateKitchenObject)this;
            return true;
        }
        return false;
    }

}
