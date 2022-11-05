using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance = null;
	private void Awake() {
		instance = this;
	}

	public AstarPath astar;
	public Vector3 spawnPoint;
	public Enemy enemyPrefab;

	public int totalEnemies;
	private List<Enemy> enemies = new List<Enemy>();
	private Camera mainCam;



	private void Start() {
		mainCam = Camera.main;
	}

	public void SpawnEnemy() {
		Enemy e = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, transform);
		e.SetCamera(mainCam);
		e.SetTarget(MapCreator.instance.GetPlayer());
		e.onDestroyed += CleanUpEnemy;
		enemies.Add(e);
		totalEnemies++;
	}

	public void RefreshPathfinding() {
		astar.Scan();
	}

	private void CleanUpEnemy(Enemy enemy) {
		enemies.Remove(enemy);
		totalEnemies--;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {

	public override void OnInspectorGUI() {
		if (GUILayout.Button("Update pathfinding")) {
			((EnemySpawner)target).RefreshPathfinding();
		}
		if (GUILayout.Button("Spawn Enemy")) {
			((EnemySpawner)target).SpawnEnemy();
		}
		base.OnInspectorGUI();
	}
}
#endif