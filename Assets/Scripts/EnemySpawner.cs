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

	public System.Action onWaveFinished;

	public AstarPath astar;
	public Vector3 spawnPoint;
	public Enemy enemyPrefab;
	public Wave[] waves;
	public List<Transform> spawnPoints = new List<Transform>();
	public float finishDelay = 2f;

	public int totalEnemies;
	private List<Enemy> enemies = new List<Enemy>();
	private Camera mainCam;

	private int currentWaveLevel;
	private int currentWaveStage = 0;



	private void Start() {
		mainCam = Camera.main;
		//StartCoroutine(spawn());
	}

	public void SendWave() {
		RefreshPathfinding();
		StartCoroutine(Spawn());
	}

	public bool WaveDone() {
		return totalEnemies == 0;
	}

	IEnumerator Spawn() {
		if (currentWaveLevel >= waves.Length)
			currentWaveLevel = waves.Length;
		totalEnemies = waves[currentWaveLevel].spawns.Count;
		yield return new WaitForSeconds(3f);

		while (waves[currentWaveLevel].spawns.Count > currentWaveStage) {
			WaveSpawn currentSpawn = waves[currentWaveLevel].spawns[currentWaveStage];
			for (int i = 0; i < currentSpawn.amount; i++) {
				int index = Random.Range(0, spawnPoints.Count);
				Vector3 spawnPos = spawnPoints[index].position;
				SpawnEnemy(currentSpawn.enemy, spawnPos);
			}
			currentWaveStage++;
			yield return new WaitForSeconds(waves[currentWaveLevel].delay);
		}
	}

	IEnumerator DelayedWaveFinished() {
		yield return new WaitForSeconds(finishDelay);
		Inventory.instance.addWood(waves[currentWaveLevel].wood);
		Inventory.instance.addFluff(waves[currentWaveLevel].fluff);
		Inventory.instance.addCandy(waves[currentWaveLevel].candy);
		currentWaveLevel++;
		onWaveFinished?.Invoke();
	}

	public void SpawnEnemy(Enemy prefab,  Vector3 pos) {
		Enemy e = Instantiate(prefab, pos, Quaternion.identity, transform);
		e.SetCamera(mainCam);
		e.SetTarget(MapCreator.instance.GetBed().transform);
		e.onDestroyed += CleanUpEnemy;
		enemies.Add(e);
	}

	public void SpawnEnemy() {
		SpawnEnemy(enemyPrefab, spawnPoint);
	}

	public void RefreshPathfinding() {
		astar.Scan();
	}

	private void CleanUpEnemy(Enemy enemy) {
		enemies.Remove(enemy);
		totalEnemies--;

		if (totalEnemies == 0) {
			StartCoroutine(DelayedWaveFinished());
		}
	}

	public List<Enemy> GetEnemies() {
		for (int i = enemies.Count - 1; i >= 0; i--) {
			if (enemies[i] == null) {
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