using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen: MonoBehaviour {

    public Food food;
    private Image foodIcon;

    private void Start()
    {
        foodIcon = transform.Find("FoodIcon").GetComponent<Image>();
    }

    public void Prepare()
    {
        foodIcon = transform.Find("FoodIcon").GetComponent<Image>();
        foodIcon.sprite = food.iconIngredientRaw;
    }
    public void startCooking()
    {

        for (int i = 0; i <= GameManager.instance.stove.Length; i++)
        {
            if (GameManager.instance.stove[i].foodStatus == Stove.FoodStatus.Empty)
            {
                GameManager.instance.stove[i].startCooking(food);
                return;
            }
        }
    }


}
