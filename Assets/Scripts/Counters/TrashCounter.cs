using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public static event EventHandler<EventArgs> OnTrashedObject;

    public static void ResetStaticData() {
        OnTrashedObject=null;
    }

    public override void Interact(Player player) {

        if(player.HasKitchenObject()) {
            OnTrashedObject?.Invoke(this, EventArgs.Empty);
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                plateKitchenObject.TrashIngredients();
            }
            else {
                player.GetKitchenObject().DestroySelf();
            }
            

            //posiciona objeto no counterTopPoint
            //dispara evento onTrash
            //[kitchenObjectAnimator escuta onTrash e ativa animação de sumir]
            //[kitchenObjectAnimator dispara evento onObjectDisappeared]
            //dúvida: quando fazer kitchenObjectAnimator disparar o evento no final da animação? Variável extra?
            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 significa o primeiro layer do Animator
            //if(stateInfo.IsName(OBJECT_DISAPPEARED)) {
            //    dispara evento onObjectDisappeared
            //}
            //trashCounter escuta onObjectDisappeared e chama DestroySelf do objeto 

           
            
        }
        
    }

}
