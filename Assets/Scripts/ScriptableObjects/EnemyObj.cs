using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/EnemyObj")]
public class EnemyObj : ScriptableObject {

    public float maxHealth, damage, attackDelay, moveSpeed;
    public List<GameObject> droppedItems = new List<GameObject>();

}
