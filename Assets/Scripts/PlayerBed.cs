using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerBed : MonoBehaviour {

	public Light2D bedLight;
	public SpriteRenderer bedSprite;
	public SpriteRenderer bedPlayerSprite;
	public Animator sleepAnim;
	public Canvas sleepCanvas;

	private bool playSleepAnimation;


	public void Setup(Camera cam) {
		bedPlayerSprite.enabled = false;
		playSleepAnimation = true;
		sleepCanvas.worldCamera = cam;
		sleepCanvas.enabled = false;
	}

	public void Upgrade(BedBuilding nextLevel) {
		sleepAnim.enabled = false;
		bedSprite.sprite = nextLevel.sprite;
		bedPlayerSprite.sprite = nextLevel.bedPlayerSprite;
		playSleepAnimation = false;
	}

	public void GoToSleep() {
		if (playSleepAnimation) {
			sleepAnim.Play("GoingToBed");
		}
		else {
			bedSprite.enabled = false;
			bedPlayerSprite.enabled = true;
		}
	}

	public void WakeUp() {
		if (playSleepAnimation) {
			sleepAnim.Play("PlayerWakingUp");
		}
		else {
			bedSprite.enabled = true;
			bedPlayerSprite.enabled = false;
		}
	}
}
