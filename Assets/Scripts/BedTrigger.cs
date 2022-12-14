using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTrigger : MonoBehaviour {

    public List<string> damageMessages = new List<string>();

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            //AudioController.instance.PlaySfx(SFX.DAMAGE);
            Enemy enemy = other.GetComponent<Enemy>();
            Inventory.instance.removeHealth(enemy.GetDamage());
            enemy.ReachedGoal();
            ChatBubble.instance.displayMessage(damageMessages[Random.Range(0, damageMessages.Count)]);
        }
    }

}
