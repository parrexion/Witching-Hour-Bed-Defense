using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public AIDestinationSetter destination;

	[Header("UI")]
	public Canvas healthCanvas;

	public System.Action<Enemy> onDestroyed;


	private void Start() {
		Destroy(gameObject, 10f);
	}

	private void OnDestroy() {
		onDestroyed(this);
	}

	public void SetCamera(Camera cam) {
		healthCanvas.worldCamera = cam;
	}

	public void SetTarget(Transform target) {
		destination.target = target;
	}
}
