using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredients;
    private List<KitchenObjectSO> currentIngredients;

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Start() {
        currentIngredients=new List<KitchenObjectSO>();
    }
    public bool TrySetIngredient(KitchenObjectSO kitchenObjectSO) {
        if(currentIngredients.Contains(kitchenObjectSO) || !validIngredients.Contains(kitchenObjectSO)) {
            return false;
        }
        currentIngredients.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO=kitchenObjectSO });
        return true;
    }
    public bool Empty() {
        return currentIngredients.Count==0;
    }

    public void TrashIngredients() {
        currentIngredients.Clear();
        Debug.Log("Probably will have to improve this later");
    }

    public List<KitchenObjectSO> GetIngredientsList() {
        return currentIngredients;
    }


}
