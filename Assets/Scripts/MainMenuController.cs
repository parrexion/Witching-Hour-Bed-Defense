using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour {

	public Slider musicSlider;
	public TMPro.TextMeshProUGUI volumeText;
	public Image fadeOutImage;
	public float fadeSpeed = 3f;

	private bool starting;


	private void Start() {
		musicSlider.value = 10;
		StartCoroutine(DelayedMusic());
		fadeOutImage.gameObject.SetActive(false);
	}

	IEnumerator DelayedMusic() {
		yield return new WaitForSeconds(1f);
		AudioController.instance.PlayMusic(Music.DAY, true);
	}

	public void StartGame() {
		if (starting)
			return;
		starting = true;
		fadeOutImage.gameObject.SetActive(true);
		fadeOutImage.color = new Color(0f, 0f, 0f, 0f);
		fadeOutImage.DOFade(1f, fadeSpeed)
			.OnComplete(() => SceneManager.LoadScene("GameScene"));
	}

	public void ChangeMusicVolume(float volume) {
		AudioController.instance.SetMusicVolume(volume * 0.1f);
		volumeText.text = volume.ToString();
	}
}
