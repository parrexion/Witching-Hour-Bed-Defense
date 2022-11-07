using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

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
	public InventoryCanvas inventoryCanvas;
	public Light2D daylight;
	public Light2D nightlight;
	public MapCreator mapCreator;
	public EnemySpawner enemySpawner;
	public Animator buildBarAnim;
	public GameObject badHouse;

	[Header("Settings")]
	public float toggleTime = 1f;
	public Vector2 lightAmount = new Vector2(0.25f, 1f);

	public List<string> dayMessages = new List<string>();

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

		badHouse.SetActive(false);

		SetDay(true);
	}

	private void SetDay(bool firstStart = false) {
		ChatBubble.instance.displayMessage(dayMessages[Random.Range(0, dayMessages.Count)]);
		Inventory.instance.heal();
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
			buildBarAnim.SetTrigger("Toggle");
			playerMove.WakeUp();
			playerBed.WakeUp();
			if (!gameOver)
				AudioController.instance.PlayMusicTransitionToDay();
		}
		else {
			AudioController.instance.PlayMusic(Music.DAY, true);
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

		AudioController.instance.PlayMusicTransitionToNight();
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
		Inventory.instance.maxHealth = nextLevel.maxHealth;
		Inventory.instance.heal();

		if (!gameOver) {
			if (CurrentLevel == 6) {
				Victory();
			}
			else {
				onUpgradeBed?.Invoke(nextLevel.awardsLevel);
			}
		}
	}

	public void ShowGameOver() {
		if (!gameOver) {
			gameOver = true;
			AudioController.instance.PlayMusicTransitionToDay();
			onGameOver?.Invoke(false);
		}
	}

	public void Victory() {
		gameOver = true;
		inventoryCanvas.statsObject.SetActive(false);
		buildBarAnim.SetTrigger("Toggle");

		playerMove.enabled = false;
		playerBuild.enabled = false;
		cameraController.enabled = false;

		Sequence seq = cameraController.ShakeFirst();
		seq.AppendInterval(1f);
		seq.AppendCallback(() => {
			inventoryCanvas.flicker.color = Color.black;
			inventoryCanvas.flicker.gameObject.SetActive(true);
		});
		seq.Append(inventoryCanvas.flicker.DOFade(0f, 1f).SetEase(Ease.Flash, 9, 1));
		seq.AppendInterval(1f);
		seq.AppendCallback(() => {
			inventoryCanvas.flicker.color = Color.black;
			AudioController.instance.PlaySfx(SFX.TRANSITION_LONG);
		});
		seq.Append(cameraController.Shake(3, 1.6f, 10));
		seq.Join(inventoryCanvas.flicker.DOFade(0f, 3f).SetEase(Ease.Flash, 8, -1));
		seq.AppendInterval(1f);
		seq.AppendCallback(() => {
			inventoryCanvas.gameObject.SetActive(false);
			cameraController.cam.orthographicSize = 18f;
			badHouse.SetActive(true);
			PlayerPrefs.SetInt("RITUAL", 1);
		});
		seq.AppendInterval(3f);
		seq.AppendCallback(() => inventoryCanvas.gameObject.SetActive(true));
		seq.Append(inventoryCanvas.foolTextGroup.DOFade(1f, 1.5f));
		seq.AppendInterval(3f);
		seq.AppendCallback(() => {
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		});

		//onGameOver?.Invoke(true);
	}

	private void WaveFinished() {
		SetDay();
	}

}
