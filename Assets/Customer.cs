using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]
public class Customer : MonoBehaviour {

    public int customerNumber;

    //public Image customerImage;
    public string name;
    public Food[] order;

    int totalGold;

    [SerializeField]
    private GameObject customerOrder;
    private int served;


    public void prepare()
    {
        for (int j = 0; j < order.Length; j++)
        {
            var _order = Instantiate(customerOrder, Vector2.zero, Quaternion.identity, transform.Find("CostumerOrder"));
            var orderData = _order.transform.GetComponent<CustomerOrder>();
            orderData.food = order[j];
            orderData.customer = this;
            totalGold = totalGold + orderData.food.price;
        }
    }

    public void ServeOrder(Food _food)
    {
        
        for (int j = 0; j < order.Length; j++)
        {
            if(order[j] == _food)
            {
                order[j] = null;
                served++;
                break;
            }
        }
        if (order.Length == served)
        {
            GameManager.instance.gold = GameManager.instance.gold + totalGold;
            GameManager.instance.goldText.text = "Gold " + GameManager.instance.gold.ToString();
            GameManager.instance.customerQueue[customerNumber] = false;
            var anim = transform.GetComponent<Animation>();
            anim.Play("CustomerOut");
            Destroy(gameObject,2);
        }
    }
}
