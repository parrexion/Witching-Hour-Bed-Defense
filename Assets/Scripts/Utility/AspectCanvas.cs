using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectCanvas : CanvasScaler {


	protected override void OnEnable() {
		base.OnEnable();
		AspectUtility.onAspectUpdated += UpdateAspectRatio;
	}

	protected override void OnDisable() {
		base.OnDisable();
		AspectUtility.onAspectUpdated -= UpdateAspectRatio;
	}

	private void UpdateAspectRatio(float aspect) {
		matchWidthOrHeight = (aspect < AspectUtility.TARGET_ASPECT_RATIO) ? 0f : 1f;
		//Debug.Log("Aspect: " + aspect);
	}
}
