using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishRack : MonoBehaviour {

    public Food food;
    private Image foodIcon;

    private void Start()
    {
        foodIcon = transform.Find("FoodIcon").GetComponent<Image>();
    }

    public void Prepare()
    {
        foodIcon = transform.Find("FoodIcon").GetComponent<Image>();
        foodIcon.sprite = food.iconPlate;
    }

    public void takePlate()
    {

        for(int i = 0; i<= GameManager.instance.plate.Length; i++)
        {
            if (GameManager.instance.plate[i].plateStatus == FoodTray.PlateStatus.Empty)
            {
                GameManager.instance.plate[i].placePlate(food);
                return;
            }
        }
    }
}
