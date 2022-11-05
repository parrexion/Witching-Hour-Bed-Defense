using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/EnemyObj")]
public class EnemyObj : ScriptableObject {

    public float maxHealth, damage, attackDelay, moveSpeed;

    [Header("Drops")]
    public int dropWood;
    public int dropFluff;
    public int dropCandy;
}
