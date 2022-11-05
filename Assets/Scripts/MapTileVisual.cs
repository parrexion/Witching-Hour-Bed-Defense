using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileVisual : MonoBehaviour {

	public SpriteRenderer tileSprite;
	public GameObject building;
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
		currentBuilding = build;
		if (build == null) {
			tileSprite.enabled = true;
			if(building != null) {
				Destroy(building);
			}
		}
		else if (build is BedBuilding) {
			tileSprite.enabled = false;
		}
		else {
			tileSprite.enabled = false;
			if(building != null) {
				Destroy(building);
			}
			building = Instantiate(build.prefab, transform.position, Quaternion.identity);
			building.transform.SetParent(transform);
		}
	}
}
