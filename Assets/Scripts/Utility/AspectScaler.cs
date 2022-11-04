using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectScaler : MonoBehaviour {

	public enum ScaleType { SCALE, RECT_SIZE }

	public ScaleType scaleType;
	public float aspectTrigger = 1.7f;
	[Header("Scaling")]
	public float bigScale = 1.5f;
	public Vector2 bigSize;

	[Header("Other features")]
	public GameObject[] bigVisuals;
	public GameObject[] smallVisuals;


	private void Awake() {
		if (Camera.main.aspect >= aspectTrigger) {
			switch (scaleType) {
				case ScaleType.SCALE:
					transform.localScale = new Vector3(bigScale, bigScale, bigScale);
					break;
				case ScaleType.RECT_SIZE:
					RectTransform rect = GetComponent<RectTransform>();
					Vector2 originalSize = rect.sizeDelta;
					rect.sizeDelta = new Vector2(originalSize.x + bigSize.x, originalSize.y + bigSize.y);
					break;
			}
			for (int i = 0; i < bigVisuals.Length; i++) {
				bigVisuals[i].SetActive(true);
			}
			for (int i = 0; i < smallVisuals.Length; i++) {
				smallVisuals[i].SetActive(false);
			}
		}
	}
}
