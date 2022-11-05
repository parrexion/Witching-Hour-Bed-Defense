using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
	private enum Type { Wood, Fluff, Candy };
	[SerializeField]
	private Type type;
	[SerializeField]
	private int amount;

	[SerializeField]
	private Rigidbody2D rb;

	private void Start() {
		rb.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(3f, 5f));
		Invoke(nameof(noVel), 0.2f);
	}
	private void noVel() {
		rb.gravityScale = 0f;
		rb.velocity = Vector3.zero;
	}
	public void setAmount(int amount) {
		this.amount = amount;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			applyCurrency();
			Destroy(gameObject);
		}
	}
	private void applyCurrency() {
		switch ((int)type) {
			case 0:
				Inventory.instance.addWood(amount);
				break;
			case 1:
				Inventory.instance.addFluff(amount);
				break;
			case 2:
				Inventory.instance.addCandy(amount);
				break;
		}
	}

}
