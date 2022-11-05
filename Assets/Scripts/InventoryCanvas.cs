using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCanvas : MonoBehaviour {

	public TextMeshProUGUI woodAmountLabel;
	public TextMeshProUGUI fluffAmountLabel;
	public TextMeshProUGUI candyAmountLabel;
	public TextMeshProUGUI healthAmountLabel;


	private void Start() {
		if (Inventory.instance != null) {
			SetText();
			Inventory.instance.onInventoryUpdated += SetText;
		}
	}

	private void SetText() {
		woodAmountLabel.text = Inventory.instance.getWood().ToString();
		fluffAmountLabel.text = Inventory.instance.getFluff().ToString();
		candyAmountLabel.text = Inventory.instance.getCandy().ToString();

		healthAmountLabel.text = (Inventory.instance.getHealth() / Inventory.instance.maxHealth * 100).ToString() + "%";
	}

	public void ClickDayButton() {
		GameState.instance.ToggleDay();
	}
}
