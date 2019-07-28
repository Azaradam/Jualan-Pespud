using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Food : ScriptableObject {

    public Sprite iconIngredientRaw;
    public Sprite iconIngredientReady;
    public Sprite iconIngredientBurned;
    public Sprite iconPlate;
    public Sprite iconFood;


    public string foodName;
    public float cookingTime;
    public int price;

}
