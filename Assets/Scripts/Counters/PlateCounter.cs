using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private int amountOfPlatesMax = 4;
    [SerializeField] private float spawnRate= 4f;
    private float spawnTimer = 0;
    private int amountOfPlates = 0;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    private void Update() {
        spawnTimer+=Time.deltaTime;

        if(amountOfPlates < amountOfPlatesMax && spawnTimer > spawnRate) {
            amountOfPlates++;
            spawnTimer=0;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            if(amountOfPlates>0) {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                amountOfPlates--;
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }

        }
        //check if player has plate
        else if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
            //check if plate has ingredient, if so break:
            if(!plateKitchenObject.Empty()) {
                return;
            }
            player.GetKitchenObject().DestroySelf();
            amountOfPlates++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);

        }

    }


}
