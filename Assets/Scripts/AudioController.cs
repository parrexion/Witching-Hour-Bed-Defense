using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Music { MAINMENU, DAY, NIGHT, }
public enum SFX { PICKUP, BUTTON, DAMAGE, }

public class AudioController : MonoBehaviour {

	public static AudioController instance = null;
	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public AudioLibrary library;
	public AudioSource[] musicSources;
	public AudioSource sfxSource;

	[Header("Settings")]
	public float fadeSpeed = 1f;

	private float musicVolume = 1f;


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
		musicSources[0].clip = library.GetMusic(music);
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

	public void PlayMusicWithTransition(Music music, Music transition) {
		AudioClip musicClip = library.GetMusic(music);
		AudioClip transitionClip = library.GetMusic(transition);
		musicSources[1].clip = transitionClip;
		musicSources[1].Play();
		Sequence seq = DOTween.Sequence();
		seq.Append(musicSources[0].DOFade(0f, fadeSpeed));
		seq.InsertCallback(transitionClip.length, () => {
			musicSources[0].clip = musicClip;
			musicSources[0].Play();
		});
		seq.Append(musicSources[0].DOFade(musicVolume, fadeSpeed));
	}

	public void PlaySfx(SFX sfx) {
		sfxSource.PlayOneShot(library.GetSfx(sfx));
	}
}
