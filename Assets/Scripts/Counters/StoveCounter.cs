using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    [SerializeField] private FryingRecipeSO[] fryingRecipesSOArray;
    private FryingRecipeSO currentRecipe;
    private float cookingProgress;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs {
        public State state;
    }

    public enum  State {
        Idle,
        Cooking,
        Burning
    }

    private State state;

    private void Update() {
        cookingProgress +=Time.deltaTime;
        //chamar função pra processar estados
        //cada estado chama função de processar estado individual
        switch(state) {
            case State.Idle:
                state=State.Idle;
                cookingProgress=0;
               // OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state=state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized=0 });
                break;
            case State.Cooking:
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized=(float)cookingProgress/(float)currentRecipe.cookingTime });
                if(cookingProgress>=currentRecipe.cookingTime) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(currentRecipe.cooked, this);
                    cookingProgress=0;
                    state=State.Burning;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state=state });
                }
                break;
            case State.Burning:
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized=(float)cookingProgress/(float)currentRecipe.burningTime });
                if(cookingProgress>=currentRecipe.burningTime) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(currentRecipe.burned, this);
                    cookingProgress=0;
                    state=State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state=state });
                }
                break;
        }
    }

    public override void Interact(Player player) {

        if(!HasKitchenObject()&&player.HasKitchenObject()) {
            //counter has no object and player does
            SetCurrentRecipe(player.GetKitchenObject().GetKitchenObjectSO());
            if(currentRecipe==null) { 
                return;
            }

            player.GetKitchenObject().SetKitchenObjectParent(this);

            state=State.Cooking;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state=state });

        }
        else if(HasKitchenObject()) {
            //counter has object
            if(!player.HasKitchenObject()) {
                //player has no object
                cookingProgress=0;
                state=State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state=state });
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
            else if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                //player has object and it's a plate
                if(plateKitchenObject.TrySetIngredient(this.GetKitchenObject().GetKitchenObjectSO())) {
                    this.GetKitchenObject().DestroySelf();
                    state=State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state=state });
                }
            }
        }

    }



    //    public override void InteractAlternate() {
    //        if(HasKitchenObject()) {
    //            //destroi o objeto
    //            //cria um novo objeto (cortado)
    //            SetCurrentRecipe(GetKitchenObject().GetKitchenObjectSO());
    //            if(currentRecipe == null) { 
    //                return; //verifica se o objeto pode ser cortado
    //            }
    //            cuttingProgress++;
    //            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized=(float)cuttingProgress/(float)currentRecipe.cuttingProgressMax });
    //            OnCuttingMovement?.Invoke(this, EventArgs.Empty);

    //            if(cuttingProgress == currentRecipe.cuttingProgressMax) {
    //                GetKitchenObject().DestroySelf();
    //                cuttingProgress=0;
    //                KitchenObject.SpawnKitchenObject(GetCutObject(), this);


    //            }
    //        }

    //    }

    //    private KitchenObjectSO GetCutObject() {
    //        return currentRecipe.output;
    //    }

    private void SetCurrentRecipe(KitchenObjectSO kitchenObjectSO) {
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipesSOArray) {
            if(fryingRecipeSO.uncooked==kitchenObjectSO) {
                currentRecipe=fryingRecipeSO;
                return;
            }
        }
        currentRecipe=null;
    }


}
