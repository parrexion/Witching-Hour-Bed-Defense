using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingEntry : MonoBehaviour {

	public Image buildingImage;
	public Image highlight;
	public Image notAffordable;


	public void SetBuilding(Building build) {
		if (build != null) {
			buildingImage.sprite = build.sprite;
			buildingImage.enabled = true;
		}
		else {
			buildingImage.enabled = false;
		}
	}

	public void SetHighlighted(bool active) {
		highlight.enabled = active;
	}

	public void SetAffordable(bool canAfford) {
		notAffordable.enabled = !canAfford;
	}
}
