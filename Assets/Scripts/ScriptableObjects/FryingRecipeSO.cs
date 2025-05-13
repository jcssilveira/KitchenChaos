using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject{
    public KitchenObjectSO uncooked;
    public KitchenObjectSO cooked;
    public KitchenObjectSO burned;
    public float cookingTime;
    public float burningTime;
}
