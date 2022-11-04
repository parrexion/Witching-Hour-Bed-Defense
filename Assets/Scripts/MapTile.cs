using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTile {

	public int x, y;
	[System.NonSerialized] public MapTile[] neighbours = new MapTile[4];


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
}
