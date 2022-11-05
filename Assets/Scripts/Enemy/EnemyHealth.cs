using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public System.Action onDeath;

	public Slider slider;
	public Image fill;
	public Gradient gradient;

	private float health;
	private float maxHealth;


	public void SetData(EnemyObj data) {
		health = maxHealth = data.maxHealth;
		slider.SetValueWithoutNotify(1f);
	}

	public void DamageEnemy(float damage) {
		//Debug.Log("Damaged " + damage);
		health -= damage;
		slider.value = (health / maxHealth);
		fill.color = gradient.Evaluate(health / maxHealth);
		if (health <= 0) {
			onDeath?.Invoke();
		}
	}

}
