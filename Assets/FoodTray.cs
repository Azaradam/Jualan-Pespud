using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTray : MonoBehaviour {

    public enum PlateStatus { Empty, plateOnly, Ready}
    public PlateStatus plateStatus;

    public Food food;
    private Image foodSlot;
    [SerializeField]
    Sprite piring;
    

    private void Start()
    {
        plateStatus = PlateStatus.Empty;
        foodSlot = transform.GetComponent<Image>();
    }

    public void placePlate(Food _food)
    {
        plateStatus = FoodTray.PlateStatus.plateOnly;
        food = _food;
        foodSlot.sprite = _food.iconPlate;
    }
    public void placeFood()
    {
        plateStatus = FoodTray.PlateStatus.Ready;
        foodSlot.sprite = food.iconFood;
    }

    public void orderTaken()
    {
        plateStatus = PlateStatus.Empty;
        food = null;
        foodSlot.sprite = piring;


    }


}
