using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilder : MonoBehaviour {


	private void Update() {
		if (Input.GetButtonDown("Jump"))
			Build();
	}

	public void Build() {
		MapTile tile = MapCreator.instance.ApproximateTile(transform.position);
		if (tile != null) {
			Building selectedBuilding = BuildUI.instance.GetCurrentBuilding();

			if (selectedBuilding == null) {
				if (tile.Blocked && !(tile.currentBuilding is BedBuilding))
					tile.AddBuilding(selectedBuilding);
			}
			else if (selectedBuilding is BedBuilding b) {
				if (Inventory.instance.CanAfford(selectedBuilding)) {
					Inventory.instance.Purchase(selectedBuilding);
					GameState.instance.UpgradeBed(b);
				}
			}
			else if (selectedBuilding != null && !tile.Blocked) {
				if (Inventory.instance.CanAfford(selectedBuilding)) {
					Inventory.instance.Purchase(selectedBuilding);
					tile.AddBuilding(selectedBuilding);
				}
			}
		}
	}
}
