using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryCanvas : MonoBehaviour {

	[Header("Day/Night")]
	public TextMeshProUGUI dayText;
	public TextMeshProUGUI dayButton;

	[Header("Stats")]
	public TextMeshProUGUI woodAmountLabel;
	public TextMeshProUGUI fluffAmountLabel;
	public TextMeshProUGUI candyAmountLabel;
	public TextMeshProUGUI healthAmountLabel;

	[Header("Game over")]
	public GameObject gameOverBlocker;
	public GameObject gameOverArea;
	public GameObject gameOverObj;
	public GameObject winObj;
	public ImageAnimator gameOverAnimator;
	public GameObject gameOverButtons;


	private void Start() {
		if (Inventory.instance != null) {
			SetStats();
			Inventory.instance.onInventoryUpdated += SetStats;
		}
		else {
			Debug.LogError("Wrong spawn order for the inventory!");
		}
		gameOverBlocker.SetActive(false);
		gameOverArea.SetActive(false);
		gameOverButtons.SetActive(false);

		gameOverAnimator.onAnimationFinished += GameOverAnimFinished;
		GameState.instance.onDayChanged += SetDayState;
		GameState.instance.onGameOver += GameOver;
	}

	private void SetStats() {
		healthAmountLabel.text = (Inventory.instance.getHealth() / Inventory.instance.maxHealth * 100).ToString() + "%";

		woodAmountLabel.text = Inventory.instance.getWood().ToString();
		fluffAmountLabel.text = Inventory.instance.getFluff().ToString();
		candyAmountLabel.text = Inventory.instance.getCandy().ToString();
	}

	private void SetDayState(bool isDay) {
		if (GameState.instance.IsDay) {
			dayText.text = "DAY";
			dayButton.text = "Go to bed";
		}
		else {
			dayText.text = $"NIGHT {GameState.instance.CurrentDay}";
			dayButton.text = "Wake up";
		}
	}

	private void GameOver(bool isVictory) {
		gameOverObj.SetActive(!isVictory);
		winObj.SetActive(isVictory);
		gameOverBlocker.SetActive(true);
	}

	public void ClickDayButton() {
		GameState.instance.ToggleDay();
	}

	public void ClickVictoryButton() {
		GameState.instance.Victory();
	}
	
	public void GoToMainButton() {
		SceneManager.LoadScene("MainMenuScene");
	}

	private void GameOverAnimFinished() {
		gameOverButtons.SetActive(true);
	}
}
