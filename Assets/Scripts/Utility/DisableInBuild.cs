using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInBuild : MonoBehaviour {

#if !UNITY_EDITOR && !UNITY_DEBUG
	private void OnEnable() {
		gameObject.SetActive(false);
	}
#endif
}
