using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSOArray;
    private CuttingRecipeSO currentRecipe;
    private int cuttingProgress;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<EventArgs> OnCut;
    
    public static event EventHandler<EventArgs> OnAnyCut;

    public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public override void Interact(Player player) {

        if(!HasKitchenObject() && player.HasKitchenObject()) {

            SetCurrentRecipe(player.GetKitchenObject().GetKitchenObjectSO());
            if(currentRecipe == null) { //verifica se o objeto pode ser cortado
                return;
            }
            cuttingProgress = 0;
            //OnAnyCut?.Invoke(this, new CuttingEventArgs { cuttingProgress=0 }); // acho que não é necessário

            player.GetKitchenObject().SetKitchenObjectParent(this);

        }
        else if (HasKitchenObject() && cuttingProgress==0){
            if(!player.HasKitchenObject()) {
                //player has no object
                this.GetKitchenObject().SetKitchenObjectParent(player); 
            }
            else if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                //player has object and it's a plate
                if(plateKitchenObject.TrySetIngredient(this.GetKitchenObject().GetKitchenObjectSO())) {
                    this.GetKitchenObject().DestroySelf();
                }
            }
        }


    }

    public override void InteractAlternate() {
        if(HasKitchenObject()) {
            //destroi o objeto
            //cria um novo objeto (cortado)
            SetCurrentRecipe(GetKitchenObject().GetKitchenObjectSO());
            if(currentRecipe == null) { 
                return; //verifica se o objeto pode ser cortado
            }
            cuttingProgress++;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized=(float)cuttingProgress/(float)currentRecipe.cuttingProgressMax });
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            if(cuttingProgress == currentRecipe.cuttingProgressMax) {
                GetKitchenObject().DestroySelf();
                cuttingProgress=0;
                KitchenObject.SpawnKitchenObject(GetCutObject(), this);


            }
        }

    }

    private KitchenObjectSO GetCutObject() {
        return currentRecipe.output;
    }

    private void SetCurrentRecipe(KitchenObjectSO kitchenObjectSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSOArray) {
            if(cuttingRecipeSO.input==kitchenObjectSO) {
                currentRecipe = cuttingRecipeSO;
                return;
            }
        }
        currentRecipe = null;
    }
}
