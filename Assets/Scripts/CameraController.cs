using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

	[SerializeField] private Camera cam;
	[SerializeField] private Vector3 offset;
	[SerializeField] private Vector2 clampOffset;
	[SerializeField] private float followCamSize;

	[Header("Animation settings")]
	[SerializeField] private float maxMoveSpeed = 1f;
	[SerializeField] private float zoomInSpeed = 1f;
	[SerializeField] private float zoomOutSpeed = 1f;

	private Transform player, bed;
	private float bedCamSize;
	public Rect clampArea;

	private bool followPlayer = true;
	private Vector3 targetPosition;


	public void Setup(Transform player, Transform bed, float bedCamSize, Rect clampArea) {
		this.player = player;
		this.bed = bed;
		this.bedCamSize = bedCamSize;
		this.clampArea = clampArea;

		this.clampArea.x = followCamSize + 1 - clampOffset.x;
		this.clampArea.y = followCamSize * 0.5f - clampOffset.y;
		this.clampArea.width += 2f * (-followCamSize + clampOffset.x);
		this.clampArea.height += 2f * clampOffset.y - followCamSize;
	}

	private void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			Toggle();
		}
		if (followPlayer) {
			targetPosition = Vector3.MoveTowards(transform.position, player.position + offset, maxMoveSpeed * Time.deltaTime);
			targetPosition.x = Mathf.Clamp(targetPosition.x, clampArea.xMin, clampArea.xMax);
			targetPosition.y = Mathf.Clamp(targetPosition.y, clampArea.yMin, clampArea.yMax);
			transform.position = targetPosition;
		}
	}

	public void Toggle() {
		followPlayer = !followPlayer;
		if (followPlayer) {
			ZoomIn();
		}
		else {
			ZoomOut();
		}
	}

	public void FollowPlayer() {
		if (!followPlayer) {
			Toggle();
		}
	}

	public void GoToOverview() {
		if (followPlayer) {
			Toggle();
		}
	}

	private void ZoomOut() {
		cam.DOOrthoSize(bedCamSize, zoomOutSpeed);
		transform.DOMove(bed.position + offset, zoomOutSpeed);
	}

	private void ZoomIn() {
		cam.DOOrthoSize(followCamSize, zoomInSpeed);
	}

}
