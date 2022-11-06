using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTower : MonoBehaviour {

    public float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            other.GetComponent<Enemy>().enemyHealth.DamageEnemy(damage);
        }
    }


}
