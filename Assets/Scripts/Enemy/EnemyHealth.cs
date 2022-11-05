using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public GameObject droppedItem;
    public Slider slider;
    public Image fill;
    public Gradient gradient;


    private float health;

    private void Start() {
        health = maxHealth;
    }

    public void damageEnemy(float damage) {
        health -= damage;
        if(health <= 0) {
            die();
        }
        slider.value = (health / (float)maxHealth);
        fill.color = gradient.Evaluate(health / (float)maxHealth);
    }
    private void die() {
        Instantiate(droppedItem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
