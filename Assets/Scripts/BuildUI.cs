using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour {

	public static BuildUI instance = null;
	private void Awake() {
		instance = this;
	}

	public BuildingLibrary buildingLibrary;
	public TemplateRecycler buildingTemplater;
	public BuildingInfoUI buildingInfo;

	private int selectedIndex;


	private void Start() {
		for (int i = 0; i < buildingLibrary.buildings.Length; i++) {
			BuildingEntry entry = buildingTemplater.CreateEntry<BuildingEntry>();
			entry.SetBuilding(buildingLibrary.buildings[i]);
		}
		RefreshHighlight();
		RefreshAfford();

		Inventory.instance.onInventoryUpdated += RefreshAfford;
	}

	private void Update() {
		if (Input.GetButtonDown("Fire1") || Input.GetAxis("Mouse ScrollWheel") > 0f) {
			ChangeBuilding(-1);
		}
		else if (Input.GetButtonDown("Fire2") || Input.GetAxis("Mouse ScrollWheel") < 0f) {
			ChangeBuilding(1);
		}
	}

	private void RefreshHighlight() {
		buildingInfo.SetInfo(buildingLibrary.buildings[selectedIndex]);
		for (int i = 0; i < buildingTemplater.Count; i++) {
			buildingTemplater.GetEntry<BuildingEntry>(i).SetHighlighted(i == selectedIndex);
		}
	}

	public void ChangeBuilding(int dir) {
		selectedIndex += dir;
		if (selectedIndex < 0)
			selectedIndex = buildingTemplater.Count - 1;
		else if (selectedIndex >= buildingTemplater.Count)
			selectedIndex = 0;

		RefreshHighlight();
	}

	public Building GetCurrentBuilding() {
		return buildingLibrary.buildings[selectedIndex];
	}

	private void RefreshAfford() {
		for (int i = 0; i < buildingTemplater.Count; i++) {
			if (buildingLibrary.buildings[i] != null) {
				bool affordable = Inventory.instance.CanAfford(buildingLibrary.buildings[i]);
				buildingTemplater.GetEntry<BuildingEntry>(i).SetAffordable(affordable);
			}
			else {
				buildingTemplater.GetEntry<BuildingEntry>(i).SetAffordable(true);
			}
		}
	}
}
