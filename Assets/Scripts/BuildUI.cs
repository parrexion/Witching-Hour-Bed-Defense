using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildUI : MonoBehaviour {

	public static BuildUI instance = null;
	private void Awake() {
		instance = this;
	}

	public BuildingLibrary buildingLibrary;
	public TemplateRecycler buildingTemplater;
	public BuildingInfoUI buildingInfo;

	private List<Building> buildingList = new List<Building>();
	private int selectedIndex;


	private void Start() {
		CreateBuildMenu(Inventory.instance.BuildLevel);

		Inventory.instance.onInventoryUpdated += RefreshAfford;
		GameState.instance.onUpgradeBed += CreateBuildMenu;
	}

	private void Update() {
		if (Input.GetButtonDown("Fire1") || Input.GetAxis("Mouse ScrollWheel") > 0f) {
			ChangeBuilding(-1);
		}
		else if (Input.GetButtonDown("Fire2") || Input.GetAxis("Mouse ScrollWheel") < 0f) {
			ChangeBuilding(1);
		}
	}

	private void CreateBuildMenu(int level) {
		selectedIndex = 0;
		buildingList.Clear();
		buildingTemplater.Clear();
		for (int i = 0; i < buildingLibrary.buildings.Length; i++) {
			if (buildingLibrary.buildings[i] != null && (buildingLibrary.buildings[i].unlockLevel > GameState.instance.CurrentLevel || buildingLibrary.buildings[i].removeLevel <= GameState.instance.CurrentLevel))
				continue;
			BuildingEntry entry = buildingTemplater.CreateEntry<BuildingEntry>();
			entry.SetBuilding(buildingLibrary.buildings[i]);
			buildingList.Add(buildingLibrary.buildings[i]);
		}
		RefreshHighlight();
		RefreshAfford();
	}

	private void RefreshHighlight() {
		buildingInfo.SetInfo(buildingList[selectedIndex]);
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
		return buildingList[selectedIndex];
	}

	private void RefreshAfford() {
		for (int i = 0; i < buildingTemplater.Count; i++) {
			if (buildingList[i] != null) {
				bool affordable = Inventory.instance.CanAfford(buildingList[i]);
				buildingTemplater.GetEntry<BuildingEntry>(i).SetAffordable(affordable);
			}
			else {
				buildingTemplater.GetEntry<BuildingEntry>(i).SetAffordable(true);
			}
		}
	}

	public void ExitButton() {
		SceneManager.LoadScene("MainMenuScene");
	}
}
