using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileVisual : MonoBehaviour {

	public SpriteRenderer sprite;
	public SpriteRenderer building;


	public void SetBuilding(Building build) {
		if (build == null) {
			building.enabled = false;
		}
		else {
			building.sprite = build.sprite;
			building.enabled = true;
		}
	}
}
