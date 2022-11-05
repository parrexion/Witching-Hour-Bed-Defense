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

	public System.Action<bool> onDayChanged;
	public System.Action<int> onUpgradeBed;
	public System.Action<bool> onGameOver;

	[Header("References")]
	public CameraController cameraController;
	public Light2D daylight;
	public Light2D nightlight;
	public MapCreator mapCreator;
	public EnemySpawner enemySpawner;
	public Animator buildBarAnim;

	[Header("Settings")]
	public float toggleTime = 1f;
	public Vector2 lightAmount = new Vector2(0.25f, 1f);

	private PlayerMovement playerMove;
	private PlayerBuilder playerBuild;
	private PlayerBed playerBed;

	private bool gameOver;
	private bool isDay = false;
	public bool IsDay => isDay;
	public int CurrentDay { get; private set; }
	public int CurrentLevel { get; private set; } = 1;


	private void Start() {
		mapCreator.CreateMap(cameraController);
		playerMove = mapCreator.GetPlayerMove();
		playerBuild = playerMove.GetComponent<PlayerBuilder>();
		playerBed = mapCreator.GetBed();
		enemySpawner.onWaveFinished += WaveFinished;

		SetDay(true);
	}

	private void SetDay(bool firstStart = false) {
		buildBarAnim.SetTrigger("Toggle");
		isDay = true;
		CurrentDay++;
		nightlight.enabled = false;
		daylight.enabled = true;
		playerBed.bedLight.enabled = false;
		DOTween.To(() => daylight.intensity, x => daylight.intensity = x, lightAmount.y, toggleTime);
		mapCreator.SetBuildMode(true);
		cameraController.FollowPlayer();
		playerMove.enabled = true;
		playerBuild.enabled = true;

		onDayChanged?.Invoke(isDay);

		if (!firstStart) {
			playerMove.WakeUp();
			playerBed.WakeUp();
		}
	}

	private void SetNight() {
		buildBarAnim.SetTrigger("Toggle");
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

		onDayChanged?.Invoke(isDay);
		enemySpawner.SendWave();
		playerMove.GoToBed();
		playerBed.GoToSleep();
	}

	public void ToggleDay() {
		if (isDay)
			SetNight();
		else
			SetDay();
	}

	public void UpgradeBed(BedBuilding nextLevel) {
		CurrentLevel = nextLevel.awardsLevel;
		playerBed.Upgrade(nextLevel);

		if (!gameOver) {
			if (CurrentLevel == 6) {
				onGameOver?.Invoke(true);
			}
			else {
				onUpgradeBed?.Invoke(nextLevel.awardsLevel);
			}
		}
	}

	public void ShowGameOver() {
		if (!gameOver) {
			gameOver = true;
			onGameOver?.Invoke(false);
		}
	}

	private void WaveFinished() {
		SetDay();
	}

}
