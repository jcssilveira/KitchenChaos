using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObjectVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;


    //create a serializefield map that maps kitchenobjetsSO to gameobjects
    [SerializeField] private Dictionary<KitchenObjectSO, GameObject> ingredientKitchenObjectSOGameObjectMap;

    [Serializable]
    public class KitchenObjectPair {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private List<KitchenObjectPair> kitchenObjectPairs;

    void Start()
    {
        //add every pair to the map
        ingredientKitchenObjectSOGameObjectMap=new Dictionary<KitchenObjectSO, GameObject>();
        foreach(KitchenObjectPair pair in kitchenObjectPairs) {
            ingredientKitchenObjectSOGameObjectMap[pair.kitchenObjectSO]=pair.gameObject;
        }

        plateKitchenObject.OnIngredientAdded+=PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        // sets active the correct ingredient gameobject
        ingredientKitchenObjectSOGameObjectMap[e.kitchenObjectSO].SetActive(true);
    }
}
