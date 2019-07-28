using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour {
    
    public enum FoodStatus { Empty, Raw, Ready, Burned }
    public FoodStatus foodStatus;

    public Food food;
    public Image foodSlot;
    [SerializeField]
    Sprite slotSprite;

    private void Start()
    {
        foodStatus = FoodStatus.Empty;
        foodSlot = transform.GetComponent<Image>();
    }

    public void startCooking(Food _food)
    {
        food = _food;
        foodStatus = FoodStatus.Raw;
        foodSlot.sprite = food.iconIngredientRaw;
        Invoke("foodReady", food.cookingTime);
    }
    public void foodReady()
    {
        foodStatus = FoodStatus.Ready;
        foodSlot.sprite = food.iconIngredientReady;
        Invoke("foodBurned", 5);
    }
    public void foodBurned()
    {
        foodStatus = FoodStatus.Burned;
        foodSlot.sprite = food.iconIngredientBurned;
    }

    public void liftFood()
    {
        
        if (foodStatus == FoodStatus.Ready)
        {
            for (int i = 0; i <= GameManager.instance.plate.Length; i++)
            {
                if (GameManager.instance.plate[i].plateStatus == FoodTray.PlateStatus.plateOnly && GameManager.instance.plate[i].food == food)
                {
                    CancelInvoke();
                    food = null;
                    foodSlot.sprite = slotSprite;
                    foodStatus = FoodStatus.Empty;
                    GameManager.instance.plate[i].placeFood();
                    return;
                }
            }
        }
        else if (foodStatus == FoodStatus.Burned)
        {
            CancelInvoke();
            food = null;
            foodSlot.sprite = slotSprite;
            foodStatus = FoodStatus.Empty;
            Debug.Log(gameObject + " Food thrown away");
        }
    }
}
