using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileVisual : MonoBehaviour {

	public SpriteRenderer tileSprite;
	public SpriteRenderer building;
	public Collider2D buildingCollider;

	private Building currentBuilding;


	public void SetBuildMode(bool active) {
		if (currentBuilding) {
			tileSprite.enabled = false;
		}
		else {
			tileSprite.enabled = active;
		}
	}

	public void SetBuilding(Building build) {
		if (build == null) {
			tileSprite.enabled = true;
			building.enabled = false;
			buildingCollider.enabled = false;
		}
		else {
			tileSprite.enabled = false;
			building.sprite = build.sprite;
			building.enabled = true;
			buildingCollider.enabled = true;
		}
	}
}
