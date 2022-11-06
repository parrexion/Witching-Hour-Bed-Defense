using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;

public class MapCreator : MonoBehaviour {

	public static MapCreator instance = null;
	private void Awake() {
		instance = this;
	}

	public Vector2Int MaxSize = new Vector2Int(10, 10);
	public Vector2 tileSize = new Vector2(1.25f, 1.25f);

	[Header("Prefabs")]
	public AstarPath astar;
	public PlayerMovement playerPrefab;
	public MapTileVisual tilePrefab;
	public PlayerBed bedPrefab;
	public BedBuilding bedBuilding;

	private PlayerMovement player;
	private PlayerBed bed;
	[SerializeField] private MapTile[] map = null;
	[SerializeField] private MapTileVisual[] mapVisuals = null;


	public void CreateMap(CameraController cam, bool forced = false) {
		if (forced) {
			CreateMap(MaxSize.x, MaxSize.y);
		}
		else {
			int pos = 0;
			for (int y = 0; y < MaxSize.y; y++) {
				for (int x = 0; x < MaxSize.x; x++) {
					map[pos].onBuildingChanged += mapVisuals[pos].SetBuilding;
					pos++;
				}
			}
		}
		bed = Instantiate(bedPrefab, new Vector3((MaxSize.x - 1) * 0.5f * tileSize.x, (MaxSize.y - 1) * 0.5f * tileSize.y, 0f), Quaternion.identity);
		player = Instantiate(playerPrefab, GetTile((MaxSize.x - 1) / 2, (MaxSize.y - 3) / 2).GetPhysicalPosition() * tileSize.x, Quaternion.identity);
		cam.Setup(player.transform, bed.transform, (Mathf.Max(MaxSize.x * 0.57f, MaxSize.y) + 1) * 0.5f * tileSize.x, new Rect(0f, 0f, MaxSize.x * tileSize.x, MaxSize.y * tileSize.y));

		//Create bed area
		bed.Setup(Camera.main);
		for (int y = MaxSize.y / 2 - 1, target = MaxSize.y / 2 + 1; y < target; y++) {
			for (int x = MaxSize.x / 2 - 1, target2 = MaxSize.x / 2 + 1; x < target2; x++) {
				MapTile tile = GetTile(x, y);
				tile.SetBed(bedBuilding);
			}
		}
	}


	public void CreateMapEditor() {
		if (mapVisuals != null) {
			for (int i = 0; i < mapVisuals.Length; i++) {
				DestroyImmediate(mapVisuals[i].gameObject);
			}
		}
		CreateMap(MaxSize.x, MaxSize.y);
	}

	private void CreateMap(int sizeX, int sizeY) {
		map = new MapTile[sizeX * sizeY];
		mapVisuals = new MapTileVisual[sizeX * sizeY];
		int pos = 0;
		for (int y = 0; y < sizeY; y++) {
			for (int x = 0; x < sizeX; x++) {
				map[pos] = new MapTile(x, y);
				if (y > 0)
					map[pos].AddNeighbour(Direction.SOUTH, map[pos - sizeX]);
				if (x > 0)
					map[pos].AddNeighbour(Direction.WEST, map[pos - 1]);
				mapVisuals[pos] = Instantiate(tilePrefab, transform);
				mapVisuals[pos].transform.position = new Vector3(x * tileSize.x, y * tileSize.y, 0);
				mapVisuals[pos].SetBuilding(null);
				map[pos].onBuildingChanged += mapVisuals[pos].SetBuilding;
				pos++;
			}
		}
	}

	public PlayerMovement GetPlayerMove() {
		return player;
	}

	public void SetBuildMode(bool active) {
		for (int i = 0; i < mapVisuals.Length; i++) {
			mapVisuals[i].SetBuildMode(active);
		}
	}
	public PlayerBed GetBed() {
		return bed;
	}

	private int GetIndex(int x, int y) {
		return x + y * MaxSize.x;
	}

	private MapTile GetTile(int x, int y) {
		return map[GetIndex(x, y)];
	}

	public MapTile ApproximateTile(Vector3 position) {
		Vector2Int approx = new Vector2Int(Mathf.RoundToInt(position.x / tileSize.x), Mathf.RoundToInt((position.y - 0.25f) / tileSize.y));
		if (approx.x < 0 || approx.x >= MaxSize.x)
			return null;
		if (approx.y < 0 || approx.y >= MaxSize.y)
			return null;

		return GetTile(approx.x, approx.y);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor : Editor {

	public override void OnInspectorGUI() {
		if (GUILayout.Button("Create map")) {
			((MapCreator)target).CreateMapEditor();
		}

		base.OnInspectorGUI();
	}
}

#endif