using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileVisual : MonoBehaviour {

	public SpriteRenderer sprite;
	public SpriteRenderer building;


	public void SetBuilding() {
		building.enabled = false;
	}
}
