using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/BedBuilding")]
public class BedBuilding : Building {

	[Header("Bed stuff")]
	public int awardsLevel = 1;
	public Sprite bedPlayerSprite;
}
