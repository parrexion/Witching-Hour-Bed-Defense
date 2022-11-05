using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered by " + other);
        if(other.CompareTag("Enemy")) {
            float damage = other.GetComponent<EnemyHealth>().enemy.damage;
            Inventory.instance.removeHealth(damage);
            Destroy(other.gameObject);
        }
    }

}
