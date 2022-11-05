using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MapCreator : MonoBehaviour {

	public static MapCreator instance = null;
	private void Awake() {
		instance = this;
	}

	public CameraController camController;
	public Vector2Int MaxSize = new Vector2Int(10, 10);

	[Header("Prefabs")]
	public AstarPath astar;
	public PlayerMovement playerPrefab;
	public MapTileVisual tilePrefab;
	public GameObject bedPrefab;

	private PlayerMovement player;
	public GameObject bed;
	private MapTile[] map;
	private MapTileVisual[] mapVisual;
	private List<GameObject> otherStuff = new List<GameObject>();


	private void Start() {
		CreateMap(MaxSize.x, MaxSize.y);
		GameObject bed = Instantiate(bedPrefab, new Vector3((MaxSize.x - 1) * 0.5f, (MaxSize.y - 1) * 0.5f, 0f), Quaternion.identity);
		this.bed = bed;
		otherStuff.Add(bed);

		player = Instantiate(playerPrefab, GetTile((MaxSize.x - 1) / 2, (MaxSize.y - 3) / 2).GetPhysicalPosition(), Quaternion.identity);
		camController.Setup(player.transform, bed.transform, (Mathf.Max(MaxSize.x * 0.57f, MaxSize.y) + 1) * 0.5f, new Rect(0f, 0f, MaxSize.x, MaxSize.y));
	}

	private void CreateMap(int sizeX, int sizeY) {
		map = new MapTile[sizeX * sizeY];
		mapVisual = new MapTileVisual[sizeX * sizeY];
		int pos = 0;
		for (int y = 0; y < sizeY; y++) {
			for (int x = 0; x < sizeX; x++) {
				map[pos] = new MapTile(x, y);
				if (y > 0)
					map[pos].AddNeighbour(Direction.SOUTH, map[pos - sizeX]);
				if (x > 0)
					map[pos].AddNeighbour(Direction.WEST, map[pos - 1]);
				mapVisual[pos] = Instantiate(tilePrefab, transform);
				mapVisual[pos].transform.position = new Vector3(x, y, 0);
				mapVisual[pos].SetBuilding(null);
				map[pos].onBuildingChanged += mapVisual[pos].SetBuilding;
				pos++;
			}
		}
	}

	public Transform GetPlayer() {
		return player.transform;
	}
	public Transform GetBed() {
		return bed.transform;
	}

	private int GetIndex(int x, int y) {
		return x + y * MaxSize.x;
	}

	private MapTile GetTile(int x, int y) {
		return map[GetIndex(x, y)];
	}

	public MapTile ApproximateTile(Vector3 position) {
		Vector2Int approx = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y - 0.25f));
		if (approx.x < 0 || approx.x >= MaxSize.x)
			return null;
		if (approx.y < 0 || approx.y >= MaxSize.y)
			return null;

		return GetTile(approx.x, approx.y);
	}
}
