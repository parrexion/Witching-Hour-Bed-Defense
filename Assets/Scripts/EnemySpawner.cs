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
	public EnemyObj defaultEnemy;
	public WaveLibrary waveLib;
	public List<Transform> spawnPoints;
	public float finishDelay = 2f;

	public int totalEnemies;
	public int totalEnemiesSpawned;
	private List<Enemy> enemies = new List<Enemy>();
	private Camera mainCam;

	public int level;
	public int stage = 0;



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
		if (level >= waveLib.waves.Length)
			level = waveLib.waves.Length - 1;
		Debug.Log("Night " + level);
		totalEnemies = 0;
		totalEnemiesSpawned = 0;
		for (int i = 0; i < waveLib.waves[level].spawns.Length; i++) {
			totalEnemies += waveLib.waves[level].spawns[i].amount;
		}
		yield return new WaitForSeconds(3f);

		stage = 0;
		while (stage < waveLib.waves[level].spawns.Length) {
			WaveData.Spawns currentSpawn = waveLib.waves[level].spawns[stage];
			for (int i = 0; i < currentSpawn.amount; i++) {
				int index = Random.Range(0, spawnPoints.Count);
				Vector3 spawnPos = spawnPoints[index].position;
				SpawnEnemy(currentSpawn.enemy, spawnPos);
				yield return new WaitForSeconds(currentSpawn.delay);
			}
			stage++;
		}
	}

	IEnumerator DelayedWaveFinished() {
		yield return new WaitForSeconds(finishDelay);
		Inventory.instance.addWood(waveLib.waves[level].wood);
		Inventory.instance.addFluff(waveLib.waves[level].fluff);
		Inventory.instance.addCandy(waveLib.waves[level].candy);
		level++;
		onWaveFinished?.Invoke();
	}

	public void SpawnEnemy(EnemyObj enemyData, Vector3 pos) {
		totalEnemiesSpawned++;
		Enemy e = Instantiate(enemyData.enemy, pos, Quaternion.identity, transform);
		e.SetCamera(mainCam);
		e.SetTarget(MapCreator.instance.GetBed().transform);
		e.onDestroyed += CleanUpEnemy;
		enemies.Add(e);
	}

	public void SpawnEnemy() {
		SpawnEnemy(defaultEnemy, spawnPoint);
	}

	public void RefreshPathfinding() {
		astar.Scan();
	}

	private void CleanUpEnemy(Enemy enemy) {
		enemies.Remove(enemy);
		totalEnemies--;
		Debug.Log(totalEnemies + " enemies left");
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