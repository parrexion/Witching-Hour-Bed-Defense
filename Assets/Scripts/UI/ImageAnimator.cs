using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour {

	public System.Action onAnimationFinished;

	public Image animateImage;
	public float animationSpeed = 0.1f;
	public Sprite[] images;

	private float time;
	private int imageIndex;


	public void Start() {
		time = 0f;
		imageIndex = 0;
		animateImage.sprite = images[imageIndex];
	}

	private void Update() {
		if (time > animationSpeed) {
			time -= animationSpeed;
			animateImage.sprite = images[imageIndex];
			imageIndex++;
			if (imageIndex >= images.Length) {
				enabled = false;
				onAnimationFinished?.Invoke();
			}
		}
		else {
			time += Time.deltaTime;
		}
	}
}
