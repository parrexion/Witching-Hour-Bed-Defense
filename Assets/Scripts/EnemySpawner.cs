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
	public Wave currentWave;
	public List<Transform> spawnPoints = new List<Transform>();

	public int totalEnemies;
	private List<Enemy> enemies = new List<Enemy>();
	private Camera mainCam;

	private int currentWaveStage = 0;



	private void Start() {
		mainCam = Camera.main;
		//StartCoroutine(spawn());
	}

	public void SendWave() {
		StartCoroutine(spawn());
	}

	IEnumerator spawn() {
		if(currentWave.spawns.Count > currentWaveStage) {
			WaveSpawn currentSpawn = currentWave.spawns[currentWaveStage];
			for(int i = 0; i < currentSpawn.amount; i++) {
				int index = Random.Range(0, spawnPoints.Count);
				Vector3 spawnPos = spawnPoints[index].position;
				SpawnEnemy(currentSpawn.enemy, spawnPos);
			}
			currentWaveStage++;
			yield return new WaitForSeconds(currentWave.delay);
			StartCoroutine(spawn());
		}
		yield return new WaitForSeconds(1f);
	}

	public void SpawnEnemy(GameObject enemy, Vector3 pos) {
		Enemy e = Instantiate(enemyPrefab, pos, Quaternion.identity, transform);
		e.SetCamera(mainCam);
		e.SetTarget(MapCreator.instance.GetBed().transform);
		e.onDestroyed += CleanUpEnemy;
		enemies.Add(e);
		totalEnemies++;
	}
	public void SpawnEnemy() {
		SpawnEnemy(enemyPrefab.gameObject, spawnPoint);
	}

	public void RefreshPathfinding() {
		astar.Scan();
	}

	private void CleanUpEnemy(Enemy enemy) {
		enemies.Remove(enemy);
		totalEnemies--;
	}

	public List<Enemy> getEnemies() {
		for(int i = enemies.Count-1; i >= 0; i--) {
			if(enemies[i] == null) {
				enemies.RemoveAt(i);
			}
		}
		return enemies;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {

	public override void OnInspectorGUI() {
		if (GUILayout.Button("Update pathfinding")) {
			((EnemySpawner)target).RefreshPathfinding();
		}
		if (GUILayout.Button("Send Wave")) {
			((EnemySpawner)target).SendWave();
		}
		if (GUILayout.Button("Spawn Enemy")) {
			((EnemySpawner)target).SpawnEnemy();
		}
		base.OnInspectorGUI();
	}
}
#endif