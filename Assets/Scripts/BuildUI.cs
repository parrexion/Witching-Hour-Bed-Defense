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

	private int selectedIndex;


	private void Start() {
		for (int i = 0; i < buildingLibrary.buildings.Length; i++) {
			BuildingEntry entry = buildingTemplater.CreateEntry<BuildingEntry>();
			entry.SetBuilding(buildingLibrary.buildings[i]);
		}
		RefreshHighlight();
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
}
