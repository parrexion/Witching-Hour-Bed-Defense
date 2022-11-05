using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Music { MAINMENU, DAY, NIGHT, }
public enum SFX { PICKUP, BUTTON, DAMAGE, }

public class AudioController : MonoBehaviour {


	public static AudioController instance = null;
	private void Awake() {
		instance = this;
	}

	public AudioLibrary library;
	public AudioSource[] musicSources;
	public AudioSource sfxSource;

	private int activeMusic;


	public void SetMusicVolume(float volume) {
		for (int i = 0; i < musicSources.Length; i++) {
			musicSources[i].volume = volume;
		}
	}

	public void SetSfxVolume(float volume) {
		sfxSource.volume = volume;
	}

	public void PlayMusic(Music music) {
		musicSources[activeMusic].clip = library.GetMusic(music);
		musicSources[activeMusic].Play();
	}

	public void PlaySfx(SFX sfx) {
		sfxSource.PlayOneShot(library.GetSfx(sfx));
	}
}
