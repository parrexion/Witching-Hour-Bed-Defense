using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Music { MAINMENU, DAY, NIGHT, }
public enum SFX { TRANSITION, PICKUP, BUTTON, DAMAGE, }

public class AudioController : MonoBehaviour {

	public static AudioController instance = null;
	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
			SetupMusic();
		}
	}

	public AudioLibrary library;
	public AudioSource[] musicSources;
	public AudioSource sfxSource;

	[Header("Settings")]
	public float fadeSpeed = 1f;
	public float transitionSpeed = 0.5f;

	private float musicVolume = 1f;


	public void SetupMusic() {
		musicSources[0].clip = library.GetMusic(Music.DAY);
		musicSources[1].clip = library.GetMusic(Music.NIGHT);
	}

	public void SetMusicVolume(float volume) {
		musicVolume = volume;
		for (int i = 0; i < musicSources.Length; i++) {
			musicSources[i].volume = volume;
		}
	}

	public void SetSfxVolume(float volume) {
		sfxSource.volume = volume;
	}

	public void PlayMusic(Music music, bool withFade = false) {
		musicSources[0].volume = musicVolume;
		//musicSources[0].clip = library.GetMusic(music);
		musicSources[0].Play();
		if (withFade) {
			musicSources[0].volume = 0f;
			musicSources[0].DOFade(musicVolume, fadeSpeed)
				.OnComplete(() => musicSources[0].volume = musicVolume);
		}
	}

	public void PlayMusicWithFadeout(Music music) {
		AudioClip musicClip = library.GetMusic(music);
		Sequence seq = DOTween.Sequence();
		seq.Append(musicSources[0].DOFade(0f, fadeSpeed));
		seq.AppendCallback(() => { musicSources[0].clip = musicClip; musicSources[0].Play(); });
		seq.Append(musicSources[0].DOFade(musicVolume, fadeSpeed));
	}

	public void PlayMusicTransitionToDay() {
		//PlaySfx(SFX.TRANSITION);
		Sequence seq = DOTween.Sequence();
		seq.Append(musicSources[1].DOFade(0f, fadeSpeed * 0.5f));
		seq.InsertCallback(fadeSpeed * 0.5f, () => {
			musicSources[1].Pause();
			musicSources[0].volume = 0f;
			musicSources[0].Play();
		});
		seq.Append(musicSources[0].DOFade(musicVolume, fadeSpeed));
	}

	public void PlayMusicTransitionToNight() {
		PlaySfx(SFX.TRANSITION);
		Sequence seq = DOTween.Sequence();
		seq.Append(musicSources[0].DOFade(0f, fadeSpeed));
		seq.InsertCallback(transitionSpeed, () => {
			musicSources[0].Pause();
			musicSources[1].volume = 0f;
			musicSources[1].Play();
		});
		seq.Append(musicSources[1].DOFade(musicVolume, fadeSpeed));
	}

	public void PlaySfx(SFX sfx) {
		sfxSource.PlayOneShot(library.GetSfx(sfx));
	}
}
