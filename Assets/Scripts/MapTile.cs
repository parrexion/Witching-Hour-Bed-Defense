using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTile {

	public System.Action<MapTile> onBuildingChanged;
	public int x, y;
	public Building currentBuilding;
	[System.NonSerialized] public MapTile[] neighbours = new MapTile[4];

	public bool alwaysBlocked;
	public bool Blocked => currentBuilding != null || alwaysBlocked;


	public MapTile (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public Vector3 GetPhysicalPosition() {
		return new Vector3(x, y, 0);
	}

	public void AddNeighbour(Direction dir, MapTile otherTile) {
		neighbours[(int)dir] = otherTile;
		otherTile.neighbours[(int)dir.Opposite()] = this;
	}

	public void AddBuilding(Building building) {
		currentBuilding = building;
		onBuildingChanged?.Invoke(this);
	}

	public void SetAlwaysBlocked(bool blocked) {
		alwaysBlocked = blocked;
		onBuildingChanged?.Invoke(this);
	}
}