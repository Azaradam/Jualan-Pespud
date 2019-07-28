using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {

    public int stoveNumber;
    public int plateNumber;
    public int goldOwned;
    public Text goldText;

    public Level level;


    [SerializeField]
    GameObject[] lockLevel;
    public bool[] levelUnlock;

    [SerializeField]
    UpgradeData[] upgradeData;

    public int[] upgradeBar;
    public int[] levelStar;
    [SerializeField]
    private Animation[] star;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        levelStar = new int[star.Length];
        LoadGame();
        goldText.text = "Gold " + goldOwned;
        for (int i = 0; i < levelUnlock.Length; i++)
        {
            if (levelUnlock[i])
            {
                lockLevel[i].SetActive(false);
                Debug.Log("TEST");
            }
            else lockLevel[i].SetActive(true);
        }
        for (int i = 0; i < levelStar.Length; i++)
        {

            if (levelStar[i] == 1)
                star[i].Play("MenuStar1");
            else if (levelStar[i] == 2)
                star[i].Play("MenuStar2");
            else if (levelStar[i] == 3)
            {
                star[i].Play("MenuStar3");
                Debug.Log("BIN3");
            }
            else star[i].Play("MenuStar0");
        }

    }
    public void levelSelect(Level _level)
    {
        level = _level;
        SceneManager.LoadScene(1);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        int i = 0;
        int j = 0;
        int k = 0;
        foreach (int item in upgradeBar)
        {
                save.upgradeCount.Add(upgradeBar[i]);
                i++;
        }
        foreach (int item in levelStar)
        {
            save.levelStar.Add(levelStar[k]);
            k++;
        }
        foreach (bool item in levelUnlock)
        {
            save.levelUnlock.Add(levelUnlock[j]);
            j++;
        }

        save.goldOwned = goldOwned;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("GAME SAVED");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < save.upgradeCount.Count; i++)
            {
                int count = save.upgradeCount[i];
                upgradeBar[i] = save.upgradeCount[i];
            }
            for (int i = 0; i < save.levelStar.Count; i++)
            {
                int count = save.levelStar[i];
                levelStar[i] = save.levelStar[i];
            }
            for (int i = 0; i < save.levelUnlock.Count; i++)
            {
                bool count = save.levelUnlock[i];
                levelUnlock[i] = save.levelUnlock[i];

            }

            //goldText.text = "Gold = " + save.goldOwned;
            goldOwned = save.goldOwned;
            goldText.text = "Gold : " + goldOwned;
            stoveNumber = upgradeData[0].upgradeEffect[upgradeBar[0]];
            plateNumber = upgradeData[1].upgradeEffect[upgradeBar[1]];
        }
        else ResetData();
    }

    public void ResetData()
    {
        int i = 0;
        foreach (int items in upgradeBar)
        {
            upgradeBar[i] = 0;
            i++;
        }
        stoveNumber = 3;
        plateNumber = 3;
        goldOwned = 0;
        goldText.text = "Gold " + goldOwned;
        SaveGame();
    }

    


}
