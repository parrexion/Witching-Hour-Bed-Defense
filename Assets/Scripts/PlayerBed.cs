using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerBed : MonoBehaviour {

	public Light2D bedLight;
	public SpriteRenderer bedSprite;
	public SpriteRenderer bedPlayerSprite;

	public BedBuilding[] upgradeLevels;


	private void Start() {
		bedPlayerSprite.enabled = false;
	}

	public void Upgrade(BedBuilding nextLevel) {
		bedSprite.sprite = nextLevel.sprite;
		bedPlayerSprite.sprite = nextLevel.bedPlayerSprite;
	}
}
