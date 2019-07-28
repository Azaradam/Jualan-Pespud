using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {

    [SerializeField]
    private UpgradeData upgradeData;
    [SerializeField]
    private LevelManager levelManager;


    [SerializeField]
    private Text upgradeNameText;
    [SerializeField]
    private Text upgradeEffectText;
    [SerializeField]
    private Text upgradePriceText;

    private void Start()
    {
        Prepare();
    }

    private void Prepare()
    {
        int upgradeCount = levelManager.upgradeBar[upgradeData.upgradeID];
        upgradeNameText.text = upgradeData.upgradeName;
       upgradeEffectText.text = upgradeData.upgradeEffect[upgradeCount].ToString();
        //upgradeEffectText.text = "" + 3;

        upgradePriceText.text = upgradeData.upgradePrice[upgradeCount].ToString();
    }

    public void BuyUpgrade()
    {
        int upgradeCount = levelManager.upgradeBar[upgradeData.upgradeID];
        if (levelManager.goldOwned >= upgradeData.upgradePrice[upgradeCount])
        {
            levelManager.upgradeBar[upgradeData.upgradeID]++;
            levelManager.goldOwned = levelManager.goldOwned - upgradeData.upgradePrice[upgradeCount];
        }
        Prepare();
        levelManager.SaveGame();
        levelManager.LoadGame();
    }
}
