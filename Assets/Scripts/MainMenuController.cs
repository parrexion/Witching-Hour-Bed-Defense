using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour {

	public Slider musicSlider;
	public Slider musicSlider2;
	public TMPro.TextMeshProUGUI volumeText;
	public TMPro.TextMeshProUGUI volumeText2;
	public Image fadeOutImage;
	public float fadeSpeed = 3f;

	public GameObject niceMenu;
	public GameObject evilMenu;

	public GameObject exitButton;
	public GameObject exitButton2;

	private bool starting;


	private void Start() {
		int ritual = PlayerPrefs.GetInt("RITUAL", 0);
		niceMenu.SetActive(ritual == 0);
		evilMenu.SetActive(ritual != 0);

		musicSlider.value = 10;
		musicSlider2.value = 10;
		StartCoroutine(DelayedMusic());
		fadeOutImage.gameObject.SetActive(false);

#if !UNITY_STANDALONE
		exitButton.SetActive(false);
		exitButton2.SetActive(false);
#endif
	}

	IEnumerator DelayedMusic() {
		yield return new WaitForSeconds(1f);
		AudioController.instance.PlayMusic(Music.DAY, true);
	}

	public void StartGame() {
		if (starting)
			return;
		AudioController.instance.PlaySfx(SFX.BUTTON);
		starting = true;
		fadeOutImage.gameObject.SetActive(true);
		fadeOutImage.color = new Color(0f, 0f, 0f, 0f);
		fadeOutImage.DOFade(1f, fadeSpeed)
			.OnComplete(() => SceneManager.LoadScene("GameScene"));
	}

	public void ChangeMusicVolume(float volume) {
		AudioController.instance.SetMusicVolume(volume * 0.1f);
		AudioController.instance.SetSfxVolume(volume * 0.1f);
		volumeText.text = volume.ToString();
		volumeText2.text = volume.ToString();
	}

	public void ExitGame() {
#if UNITY_STANDALONE
		Application.Quit();
#endif
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
