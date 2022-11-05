using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileVisual : MonoBehaviour {

	public SpriteRenderer tileSprite;
	public SpriteRenderer building;
	public Collider2D buildingCollider;


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
