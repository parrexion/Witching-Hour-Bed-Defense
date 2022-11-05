using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoUI : MonoBehaviour {

	public Image buildingImage;
	public TMPro.TextMeshProUGUI nameText;
	public TMPro.TextMeshProUGUI descText;

	public GameObject costArea;
	public TMPro.TextMeshProUGUI woodText;
	public TMPro.TextMeshProUGUI fluffText;
	public TMPro.TextMeshProUGUI candyText;



	public void SetInfo(Building build) {
		if (build == null) {
			buildingImage.enabled = false;
			nameText.text = "Clear";
			descText.text = "Remove an existing building";
			costArea.SetActive(false);
		}
		else {
			buildingImage.sprite = build.sprite;
			buildingImage.enabled = true;
			nameText.text = build.label;
			descText.text = build.desc;
			costArea.SetActive(true);
			woodText.text = build.woodCost.ToString();
			fluffText.text = build.fluffCost.ToString();
			candyText.text = build.candyCost.ToString();
		}
	}
}
