using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

	public static MapCreator instance = null;
	private void Awake() {
		instance = this;
	}

	public Camera cam;
	public Vector2Int MaxSize = new Vector2Int(10, 10);

	[Header("Prefabs")]
	public PlayerMovement playerPrefab;
	public MapTileVisual tilePrefab;
	public GameObject bedPrefab;

	private PlayerMovement player;
	private MapTile[] map;
	private MapTileVisual[] mapVisual;
	private List<GameObject> otherStuff = new List<GameObject>();


	private void Start() {
		cam.transform.position = new Vector3((MaxSize.x - 1) * 0.5f, (MaxSize.y - 1) * 0.5f, -10f);
		cam.orthographicSize = (Mathf.Max(MaxSize.x, MaxSize.y) + 1) * 0.5f;
		CreateMap(MaxSize.x, MaxSize.y);
		GameObject bed = Instantiate(bedPrefab, new Vector3((MaxSize.x - 1) * 0.5f, (MaxSize.y - 1) * 0.5f, 0f), Quaternion.identity, transform);
		otherStuff.Add(bed);
		player = Instantiate(playerPrefab, GetTile((MaxSize.x - 1) / 2, (MaxSize.y - 3) / 2).GetPhysicalPosition(), Quaternion.identity, transform);
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
