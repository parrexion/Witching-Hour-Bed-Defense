using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyObj")]
public class EnemyObj : ScriptableObject {

	public Enemy enemy;

	[Header("Stats")]
	public int maxHealth;
	public int damage;
	public float attackDelay, moveSpeed;

	[Header("Drops")]
	public int dropWood;
	public int dropFluff;
	public int dropCandy;
}
