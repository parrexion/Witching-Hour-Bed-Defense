using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public Slider musicSlider;
	public TMPro.TextMeshProUGUI volumeText;


	private void Start() {
		musicSlider.value = 10;
		StartCoroutine(DelayedMusic());
	}

	IEnumerator DelayedMusic() {
		yield return new WaitForSeconds(1f);
		AudioController.instance.PlayMusic(Music.DAY, true);
	}

	public void StartGame() {
		SceneManager.LoadScene("GameScene");
	}

	public void ChangeMusicVolume(float volume) {
		AudioController.instance.SetMusicVolume(volume * 0.1f);
		volumeText.text = volume.ToString();
	}
}
