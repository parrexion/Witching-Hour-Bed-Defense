using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(RectTransform))]
public class ImageOrganizer : MonoBehaviour {

	public enum SortDirection { LEFTRIGHT, TOPBOT, RIGHTLEFT, BOTTOP }
	public enum Alignment { CENTER, START, END }

	private const float ORGANIZER_UPDATE = 0.25f;

	public Vector2 itemSize = new Vector2(110f, 110f);
	public SortDirection sortDirection;
	public Alignment alignment;
	public float spacing;
	public bool maxSpacing;
	public Vector2 padding;
	public Vector2 itemOffset;

	public float CurrentStep { get; private set; }
	private RectTransform rectTransform;
	private Vector2 middlePivot = new Vector2(0.5f, 0.5f);
	private Vector2 fullAnchor = new Vector2(0f, 1f);
	private Sequence seq;


	private void OnEnable() {
		UpdateLayout();
	}

	public Sequence UpdateLayout(TemplateRecycler templater, bool animate = true) {
		List<RectTransform> rects = templater.GetAllEntries<RectTransform>();
		return Refresh(rects, animate);
	}

	public Sequence UpdateLayout(bool animate = true) {
		int childs = transform.childCount;
		List<RectTransform> rects = new List<RectTransform>();
		for (int i = 0; i < childs; i++) {
			if (transform.GetChild(i).gameObject.activeSelf) {
				rects.Add((RectTransform)transform.GetChild(i));
			}
		}
		return Refresh(rects, animate);
	}

	private Sequence Refresh(List<RectTransform> rects, bool animate) { 
		rectTransform = transform.GetComponent<RectTransform>();
		int rectCount = rects.Count;
		//seq.Kill();
		seq = DOTween.Sequence();

		//Debug.Log("REFRESH:  " + rectCount);
		if (sortDirection == SortDirection.LEFTRIGHT || sortDirection == SortDirection.RIGHTLEFT) {
			float fullWidth = rectTransform.rect.width - padding.x - padding.y - itemSize.x;
			CurrentStep = (rectCount > 1) ? fullWidth / (rectCount - 1) : fullWidth;
			if (!maxSpacing)
				CurrentStep = Mathf.Min(spacing + itemSize.x, CurrentStep);

			float startOffset = 0f;
			if (alignment == Alignment.CENTER)
				startOffset = (rectCount - 1) * 0.5f * CurrentStep;
			else if (alignment == Alignment.START)
				startOffset = (rectTransform.rect.width - itemSize.x) * 0.5f - padding.x;

			for (int i = 0; i < rectCount; i++) {
				rects[i].pivot = middlePivot;
				rects[i].anchorMin = middlePivot;
				rects[i].anchorMax = middlePivot;
				Vector2 pos = new Vector2(
					itemOffset.x + ((sortDirection == SortDirection.LEFTRIGHT) ? CurrentStep * i - startOffset : startOffset - CurrentStep * i),
					itemOffset.y
				);
				//if(animate)
				//	seq.Join(rects[i].DOAnchorPos(pos, ORGANIZER_UPDATE));
				//else
				//	rects[i].anchoredPosition = pos;
			}
		}
		else if (sortDirection == SortDirection.TOPBOT || sortDirection == SortDirection.BOTTOP) {
			float fullHeight = rectTransform.rect.height - padding.x - padding.y - itemSize.y;
			CurrentStep = (rectCount > 1) ? fullHeight / (rectCount - 1) : fullHeight;
			if (!maxSpacing)
				CurrentStep = Mathf.Min(spacing + itemSize.y, CurrentStep);

			float startOffset = 0f;
			if (alignment == Alignment.CENTER)
				startOffset = (rectCount - 1) * 0.5f * CurrentStep;
			else if (alignment == Alignment.START)
				startOffset = (rectTransform.rect.height - itemSize.y) * 0.5f - padding.x;

			for (int i = 0; i < rectCount; i++) {
				rects[i].pivot = middlePivot;
				rects[i].anchorMin = middlePivot;
				rects[i].anchorMax = middlePivot;
				Vector2 pos = new Vector2(
					itemOffset.x,
					itemOffset.y + ((sortDirection == SortDirection.TOPBOT) ? startOffset - CurrentStep * i : CurrentStep * i - startOffset)
				);
				//if(animate)
				//	seq.Join(rects[i].DOAnchorPos(pos, ORGANIZER_UPDATE));
				//else
				//	rects[i].anchoredPosition = pos;
			}
		}
		return seq;
	}
}


#if UNITY_EDITOR

[CustomEditor(typeof(ImageOrganizer)), CanEditMultipleObjects]
public class ImageOrganizerEditor : Editor {

	public override void OnInspectorGUI() {
		if (targets.Length == 1) {
			if (GUILayout.Button("Update layout")) {
				((ImageOrganizer)target).UpdateLayout(false);
			}
			GUILayout.Label("Current spacing: " + ((ImageOrganizer)target).CurrentStep);
		}

		base.OnInspectorGUI();
	}
}

#endif