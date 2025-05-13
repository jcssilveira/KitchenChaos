using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private int amountOfPlatesMax = 4;
    [SerializeField] private float spawnRate= 4f;
    private float spawnTimer = 0;
    private int amountOfPlates = 0;

    public event EventHandler<OnOrderDeliveredEventArgs> OnOrderDelivered;
    public class OnOrderDeliveredEventArgs : EventArgs {
        public List<KitchenObjectSO> ingredients;
    }

    public override void Interact(Player player) {
        ////check if player has plate
        ///
        //if so, delete plate and shoot event OnOrderDelivered passing ingredients
        if(!player.HasKitchenObject()) {
            return;

        }
        //check if player has plate
        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
            if(plateKitchenObject.Empty()) {
                return;
            }
            player.GetKitchenObject().DestroySelf();
            OnOrderDelivered?.Invoke(this, new OnOrderDeliveredEventArgs { ingredients=plateKitchenObject.GetIngredientsList() });

        }

    }


}
