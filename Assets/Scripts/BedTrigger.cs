using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            Inventory.instance.removeHealth(enemy.GetDamage());
            enemy.ReachedGoal();
            ChatBubble.instance.displayMessage("Nice tower placement Einstein...");
        }
    }

}
