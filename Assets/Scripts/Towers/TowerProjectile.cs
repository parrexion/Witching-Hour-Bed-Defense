using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {

    public float damage;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
