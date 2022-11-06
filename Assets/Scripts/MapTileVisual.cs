using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapTileVisual : MonoBehaviour {

	public int x, y;
	public SpriteRenderer tileSprite;
	public GameObject building;
	public Collider2D buildingCollider;

	private Building currentBuilding;
	public bool alwaysBlocked;


	public void SetPos(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public void SetBuildMode(bool active) {
		if (currentBuilding) {
			tileSprite.enabled = false;
		}
		else {
			tileSprite.enabled = active && !alwaysBlocked;
		}
	}

	public void SetBuilding(MapTile tile) {
		currentBuilding = tile.currentBuilding;
		alwaysBlocked = tile.alwaysBlocked;
		if (tile.alwaysBlocked) {
			tileSprite.enabled = false;
		}
		else if (!tile.Blocked) {
			tileSprite.enabled = true;
			if(building != null) {
				Destroy(building);
			}
		}
		else {
			tileSprite.enabled = false;
			if(building != null) {
				Destroy(building);
			}
			building = Instantiate(currentBuilding.prefab, transform.position, Quaternion.identity, transform);
		}
	}
}



#if UNITY_EDITOR

[CustomEditor(typeof(MapTileVisual))]
public class MapTileVisualEditor : Editor {

	public override void OnInspectorGUI() {
		if (GUILayout.Button("Toggle blocked")) {
			MapTileVisual vis = (MapTileVisual)target;
			vis.alwaysBlocked = !vis.alwaysBlocked;
			MapCreator map = FindObjectOfType<MapCreator>();
			MapTile tile = map.SetAlwaysBlocked(vis.x, vis.y, vis.alwaysBlocked);
			vis.SetBuilding(tile);
		}

		base.OnInspectorGUI();

	}
}

#endif