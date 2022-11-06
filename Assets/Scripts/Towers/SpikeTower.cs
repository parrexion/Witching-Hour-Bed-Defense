using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTower : MonoBehaviour {

    public float damage;

	private void Start() {
        GameState.instance.onDayChanged += TakeDown;

    }

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().enemyHealth.DamageEnemy(damage);
        }
    }

    private void TakeDown(bool isDay) {
        if (isDay) {
            GameState.instance.onDayChanged -= TakeDown;
            GetComponentInParent<MapTileVisual>().RemoveBuilding();
        }
	}

}
