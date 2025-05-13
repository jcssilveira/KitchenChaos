using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ClearCounter : BaseCounter {


    //[SerializeField] private KitchenObjectSO kitchenObjectSO; NÃO PRECISO MAIS DISSO?

    public override void Interact(Player player) {

        if(!HasKitchenObject() && player.HasKitchenObject()) {
            player.GetKitchenObject().SetKitchenObjectParent(this);

        }
        else if (HasKitchenObject()){
            //counter has object
            if(!player.HasKitchenObject()) {
                //player has no object
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
            else if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                if(plateKitchenObject.TrySetIngredient(this.GetKitchenObject().GetKitchenObjectSO())) {
                    this.GetKitchenObject().DestroySelf();
                }
            }
            else if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                //counter has object and it's a plate
                if(plateKitchenObject.TrySetIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().DestroySelf();
                }
            }

        }

    }


}
