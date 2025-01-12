﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject {

    public int levelIndex;
    public GameObject[] customer;
    public Food[] food;

    public int star1, star2, star3;
}
