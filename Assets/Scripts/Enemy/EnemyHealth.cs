using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public EnemyObj enemy;

    public Slider slider;
    public Image fill;
    public Gradient gradient;


    private float health;

    private void Start() {
        health = enemy.maxHealth;
    }

    public void damageEnemy(float damage) {
        health -= damage;
        if(health <= 0) {
            die();
        }
        slider.value = (health / (float)enemy.maxHealth);
        fill.color = gradient.Evaluate(health / (float)enemy.maxHealth);
    }
    private void die() {
        int i = Random.Range(0, enemy.droppedItems.Count);
        Instantiate(enemy.droppedItems[i], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
