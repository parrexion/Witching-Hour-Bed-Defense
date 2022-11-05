using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTrigger : MonoBehaviour {

	public Canvas canvas;


	private void Start() {
		GameState.instance.onDayChanged += TurnOffCanvas;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			canvas.enabled = true;
			PlayerMovement player = collision.GetComponent<PlayerMovement>();
			player.SetCanSleep(true);
			Debug.Log("Now inside");
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			canvas.enabled = false;
			PlayerMovement player = collision.GetComponent<PlayerMovement>();
			player.SetCanSleep(false);
			Debug.Log("Now outside");
		}
	}

	private void TurnOffCanvas(bool _) {
		canvas.enabled = false;
	}
}
