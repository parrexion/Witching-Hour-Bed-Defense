using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCanvas : MonoBehaviour {

	[Header("Day/Night")]
	public TextMeshProUGUI dayText;
	public TextMeshProUGUI dayButton;

	[Header("Stats")]
	public TextMeshProUGUI woodAmountLabel;
	public TextMeshProUGUI fluffAmountLabel;
	public TextMeshProUGUI candyAmountLabel;
	public TextMeshProUGUI healthAmountLabel;


	private void Start() {
		if (Inventory.instance != null) {
			SetStats();
			Inventory.instance.onInventoryUpdated += SetStats;
		}
		else {
			Debug.LogError("Wrong spawn order for the inventory!");
		}
		GameState.instance.onDayChanged += SetDayState;
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

	public void ClickDayButton() {
		GameState.instance.ToggleDay();
	}
}
