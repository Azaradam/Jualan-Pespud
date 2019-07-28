using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour {

    public Food food;
    public Customer customer;

    private void Start()
    {
        var _sprite = transform.GetComponent<Image>();
        _sprite.sprite = food.iconFood;
    }

    public void ServerOder()
    {
        for(int i = 0; i < GameManager.instance.plate.Length; i++)
            {
            if (GameManager.instance.plate[i].plateStatus == FoodTray.PlateStatus.Ready && GameManager.instance.plate[i].food == food)
            {
                GameManager.instance.plate[i].orderTaken();
                customer.ServeOrder(food);
                
                Destroy(gameObject);
                return;
            }
        }
    }
    
}
