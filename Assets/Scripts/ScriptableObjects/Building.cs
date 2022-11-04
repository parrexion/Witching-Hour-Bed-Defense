using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Building")]
public class Building : ScriptableObject {

    public string label, desc;
    public int woodCost, fluffCost, candyCost;
    public GameObject prefab;
    public Sprite sprite;

}
