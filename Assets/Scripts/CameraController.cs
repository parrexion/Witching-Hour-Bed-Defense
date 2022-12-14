using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

	public Camera cam;
	[SerializeField] private Vector3 offset;
	[SerializeField] private Vector2 clampOffset;
	[SerializeField] private float followCamSize;
	[SerializeField] private float bedCamSizeOffset;

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
		this.bedCamSize = bedCamSize + bedCamSizeOffset;
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

	public Sequence ShakeFirst() {
		AudioController.instance.StopMusic();
		AudioController.instance.PlaySfx(SFX.TRANSITION);
		Sequence seq = DOTween.Sequence();
		seq.Append(cam.DOOrthoSize(bedCamSize, zoomOutSpeed));
		seq.Join(transform.DOMove(bed.position + offset, zoomOutSpeed));
		seq.Append(cam.DOShakePosition(2f, 0.8f, 5, fadeOut: false));
		return seq;
	}

	public Tween Shake(float duration, float strength, int vibrato) {
		return cam.DOShakePosition(duration, strength, vibrato, fadeOut: false);
	}

	private void ZoomOut() {
		cam.DOOrthoSize(bedCamSize, zoomOutSpeed);
		transform.DOMove(bed.position + offset, zoomOutSpeed);
	}

	private void ZoomIn() {
		cam.DOOrthoSize(followCamSize, zoomInSpeed);
	}

}
