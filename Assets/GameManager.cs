using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("WARNING! : Multiple instance detected");
            return;
        }
        instance = this;
    }

    #endregion

    private LevelManager levelManager;

    [SerializeField]
    private Transform[] customerQueuePos; //Customer Spawn Position
    public bool[] customerQueue;

    [SerializeField]
    private GameObject customerPanel;

    [SerializeField]
    private GameObject[] customer;

    [SerializeField]
    int customerNumber = 0;

    public Food[] food;
    public Kitchen[] kitchen;
    public DishRack[] dishRack;

    

    [SerializeField]
    private GameObject stoveParent;
    [SerializeField]
    private Transform[] stovePosition;
    [SerializeField]
    private GameObject stovePrefab;
    public Stove[] stove;

    [SerializeField]
    private GameObject plateParent;
    [SerializeField]
    private Transform[] platePosition;
    [SerializeField]
    private GameObject platePrefab;
    public FoodTray[] plate;

    [SerializeField]
    Text timeText;
    float time;

    public int gold;
    public Text goldText;

    bool win;
    [SerializeField]
    GameObject winCanvas, timeUpText, star;
    [SerializeField]
    Text goldEarned;
    [SerializeField]
    Animation starEarned;
    [SerializeField]
    GameObject backToMenuButton;





    private void Start()
    {
        time = 60;
        win = false;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        customerQueue = new bool[customerQueuePos.Length];
        food = levelManager.level.food;
        for (int i = 0; i < food.Length; i++)
        {
            kitchen[i].food = food[i];
            kitchen[i].Prepare();
            dishRack[i].food = food[i];
            dishRack[i].Prepare();
        }
        stovePosition = stoveParent.GetComponentsInChildren<Transform>();
        platePosition = plateParent.GetComponentsInChildren<Transform>();
        for (int i = 1; i < levelManager.stoveNumber+1 ; i++)
        {
            Instantiate(stovePrefab, stovePosition[i].position, Quaternion.identity, stoveParent.transform);
        }
        for (int i = 1; i < levelManager.plateNumber + 1; i++)
        {
            Instantiate(platePrefab, platePosition[i].position, Quaternion.identity, plateParent.transform);
        }
        stove = stoveParent.GetComponentsInChildren<Stove>();
        plate = plateParent.GetComponentsInChildren<FoodTray>();

        customer = levelManager.level.customer;
        InvokeRepeating("SpawnCustomer", 0, 2);
    }

    private void Update()
    {
        if(time > 0)
        time -= Time.deltaTime;
        else if(time <= 0 && !win)
        {
            win = true;
            StartCoroutine("TimeUp");
        }
        timeText.text = time.ToString("f0");
    }

    private void SpawnCustomer()
    {

        for (int i = 0; i < customerQueue.Length; i++)
        {
            if (!customerQueue[i])
            {
                var num = Random.Range(0, customer.Length);
                var _customer = Instantiate(customer[num], customerQueuePos[i].transform.position, Quaternion.identity, customerPanel.transform);
                customerQueue[i] = !customerQueue[i];
                _customer.GetComponent<Customer>().prepare();
                _customer.GetComponent<Customer>().customerNumber = i;
                return;
            }
        }

        
    }

    IEnumerator TimeUp()
    {
        winCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        timeUpText.SetActive(false);
        goldEarned.text = "Gold Earned " + gold;
        goldEarned.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        if(gold >= levelManager.level.star3)
        {
            starEarned.Play("Star3");
            if(levelManager.levelStar[levelManager.level.levelIndex-1] <= 3)
            levelManager.levelStar[levelManager.level.levelIndex-1] = 3;
            levelManager.levelUnlock[levelManager.level.levelIndex] = true;
        }
        else if(gold >= levelManager.level.star2)
            {
            starEarned.Play("Star2");
            if (levelManager.levelStar[levelManager.level.levelIndex-1] <= 2)
                levelManager.levelStar[levelManager.level.levelIndex-1] = 2;
            levelManager.levelUnlock[levelManager.level.levelIndex] = true;
        }
        else if (gold >= levelManager.level.star1)
        {
            starEarned.Play("Star1");
            if (levelManager.levelStar[levelManager.level.levelIndex-1] <= 1)
                levelManager.levelStar[levelManager.level.levelIndex-1] = 1;
            levelManager.levelUnlock[levelManager.level.levelIndex] = true;
        }
        else
        {
            starEarned.Play("Star0");
        }
        yield return new WaitForSeconds(1);
        backToMenuButton.SetActive(true);


    }

    public void BacktoMainMenu()
    {
        levelManager.goldOwned = levelManager.goldOwned + gold;
        levelManager.SaveGame();
        Destroy(levelManager.gameObject);
        SceneManager.LoadScene(0);
    }



}
