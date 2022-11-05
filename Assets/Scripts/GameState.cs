using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GameState : MonoBehaviour {

	public static GameState instance = null;
	private void Awake() {
		instance = this;
	}

	[Header("References")]
	public CameraController cameraController;
	public Light2D daylight;
	public Light2D nightlight;
	public MapCreator mapCreator;
	public EnemySpawner enemySpawner;

	[Header("Settings")]
	public float toggleTime = 1f;
	public Vector2 lightAmount = new Vector2(0.25f, 1f);

	private PlayerMovement playerMove;
	private PlayerBuilder playerBuild;
	private PlayerBed playerBed;

	private bool isDay = false;


	private void Start() {
		mapCreator.CreateMap(cameraController);
		playerMove = mapCreator.GetPlayerMove();
		playerBuild = playerMove.GetComponent<PlayerBuilder>();
		playerBed = mapCreator.GetBed();

		SetDay();
	}

	private void SetDay() {
		isDay = true;
		nightlight.enabled = false;
		daylight.enabled = true;
		playerBed.bedLight.enabled = false;
		DOTween.To(() => daylight.intensity, x => daylight.intensity = x, lightAmount.y, toggleTime);
		mapCreator.SetBuildMode(true);
		cameraController.FollowPlayer();
		playerMove.enabled = true;
		playerBuild.enabled = true;
	}

	private void SetNight() {
		isDay = false;
		DOTween.To(() => daylight.intensity, x => daylight.intensity = x, lightAmount.x, toggleTime)
			.OnComplete(() => {
				daylight.enabled = false;
				nightlight.enabled = true;
				playerBed.bedLight.enabled = true;
			});
		mapCreator.SetBuildMode(false);
		cameraController.GoToOverview();
		playerMove.enabled = false;
		playerBuild.enabled = false;

		//enemySpawner.
	}

	public void ToggleDay() {
		if (isDay)
			SetNight();
		else
			SetDay();
	}

}
