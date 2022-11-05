using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Building")]
public class Building : ScriptableObject {

    public GameObject prefab;
    public bool blocking = true;

    [Header("Menu info")]
    public string label, desc;
    public Sprite sprite;
    public int unlockLevel;
    public int removeLevel = 99;

    [Header("Cost")]
    public int woodCost;
    public int fluffCost;
    public int candyCost;

}
