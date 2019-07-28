using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {

    public int goldOwned;
    public List<int> upgradeCount = new List<int>();
    public List<bool> levelUnlock = new List<bool>();
    public List<int> levelStar = new List<int>();


}
