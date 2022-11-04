using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

	public Camera cam;
	public Vector2Int MaxSize = new Vector2Int(10, 10);

	[Header("Prefabs")]
	public MapTileVisual tilePrefab;
	public GameObject bedPrefab;

	private MapTile[] map;
	private MapTileVisual[] mapVisual;
	private List<GameObject> otherStuff;


	private void Start() {
		cam.transform.position = new Vector3((MaxSize.x - 1) * 0.5f, (MaxSize.y - 1) * 0.5f, -10f);
		cam.orthographicSize = (Mathf.Max(MaxSize.x, MaxSize.y) + 1) * 0.5f;
		CreateMap(MaxSize.x, MaxSize.y);
		GameObject bed = Instantiate(bedPrefab, cam.transform.position, Quaternion.identity, transform);
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
}
