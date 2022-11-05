using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public System.Action<Enemy> onDestroyed;

	[SerializeField] private EnemyObj enemyData;
	public EnemyHealth enemyHealth;
	public AIDestinationSetter destination;
	public ItemPickUp dropWoodPrefab;
	public ItemPickUp dropFluffPrefab;
	public ItemPickUp dropCandyPrefab;

	[Header("UI")]
	public Canvas healthCanvas;




	private void Start() {
		enemyHealth.SetData(enemyData);
		enemyHealth.onDeath += Die;
	}

	public void SetCamera(Camera cam) {
		healthCanvas.worldCamera = cam;
	}

	public void SetTarget(Transform target) {
		destination.target = target;
	}

	public void SetData(EnemyObj enemy) {
		enemyData = enemy;
		enemyHealth.SetData(enemy);
	}

	public float GetDamage() {
		return enemyData.damage;
	}

	public void TakeDamage(float damage) {
		enemyHealth.DamageEnemy(damage);
	}

	public void Die() {
		if (enemyData.dropWood > 0) {
			ItemPickUp pickup = Instantiate(dropWoodPrefab, transform.position, Quaternion.identity);
			pickup.setAmount(enemyData.dropWood);
		}
		if (enemyData.dropFluff > 0) {
			ItemPickUp pickup = Instantiate(dropFluffPrefab, transform.position, Quaternion.identity);
			pickup.setAmount(enemyData.dropFluff);
		}
		if (enemyData.dropCandy > 0) {
			ItemPickUp pickup = Instantiate(dropCandyPrefab, transform.position, Quaternion.identity);
			pickup.setAmount(enemyData.dropCandy);
		}
		onDestroyed(this); 
		Destroy(gameObject);
	}

	public void ReachedGoal() {
		onDestroyed(this);
		Destroy(gameObject);
	}
}
