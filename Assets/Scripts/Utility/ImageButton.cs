using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour, IPointerClickHandler
#if UNITY_ANDROID
	,IPointerDownHandler, IPointerUpHandler 
#endif
	{

	public bool interactable = true;

	public System.Action<PointerEventData> onLeftClick;
	public System.Action<PointerEventData> onRightClick;

	[HideInInspector] public bool hasBeenLongPressed;
	private CanvasGroup groupCache;
	private Transform transformTarget;


	void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
		if (interactable && CheckInteractable() && !hasBeenLongPressed) {
			if (eventData.button == PointerEventData.InputButton.Left) {
				onLeftClick?.Invoke(eventData);
			}
			else if (eventData.button == PointerEventData.InputButton.Right) {
				onRightClick?.Invoke(eventData);
			}
		}
		hasBeenLongPressed = false;
	}

	private bool CheckInteractable() {
		transformTarget = transform;
		while (transformTarget != null) {
			groupCache = transformTarget.GetComponent<CanvasGroup>();
			if (groupCache != null) {
				if (groupCache.ignoreParentGroups)
					return true;
				if (!groupCache.interactable)
					return false;
			}
			transformTarget = transformTarget.parent;
		}
		return true;
	}


#if UNITY_ANDROID
	private bool pressed;
	void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
		pressed = true;
		LongClickMaster.Instance.PressDown(this, eventData);
		hasBeenLongPressed = false;
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
		if (pressed) {
			pressed = false;
			LongClickMaster.Instance.Release(this);
		}
	}
#endif

}
