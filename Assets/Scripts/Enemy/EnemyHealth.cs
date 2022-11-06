using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public System.Action onDeath;

	public SpriteRenderer sprt;
	public Slider slider;
	public Image fill;
	public Gradient gradient;

	private float health;
	private float maxHealth;
	private Color initialColor;


	public void SetData(EnemyObj data) {
		health = maxHealth = data.maxHealth;
		slider.SetValueWithoutNotify(1f);
		initialColor = sprt.color;
	}

	public void DamageEnemy(float damage) {
		health -= damage;
		slider.value = (health / maxHealth);
		fill.color = gradient.Evaluate(health / maxHealth);
		if (health <= 0) {
			onDeath?.Invoke();
		}
		sprt.color = new Color(1, 0.5f, 0.5f, 1f);
		Invoke("noRed", 0.05f);
	}
	private void noRed() {
		sprt.color = initialColor;
	}

}
