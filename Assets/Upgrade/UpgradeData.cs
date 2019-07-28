using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeData : ScriptableObject {
    public int upgradeID;
    public string upgradeName;
    public int[] upgradePrice;
    public int[] upgradeEffect;
}
