using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/WaveData")]
public class WaveData : ScriptableObject {

    [System.Serializable]
    public class Spawns {
        public EnemyObj enemy;
        public int amount;
        public float delay;
    }

    public Spawns[] spawns;

    [Header("Clear rewards")]
    public int wood;
    public int fluff;
    public int candy;
}
