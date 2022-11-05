using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Wave")]
public class Wave : ScriptableObject {

    public List<WaveSpawn> spawns = new List<WaveSpawn>();
    public float delay;

    [Header("Clear rewards")]
    public int wood;
    public int fluff;
    public int candy;
}
