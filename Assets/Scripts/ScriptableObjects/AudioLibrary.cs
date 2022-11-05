using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/AudioLibrary")]
public class AudioLibrary : ScriptableObject {

	public AudioClip[] musicClips;
	public AudioClip[] sfxClips;


	public AudioClip GetMusic(Music music) {
		return sfxClips[(int)music];
	}

	public AudioClip GetSfx(SFX sfx) {
		return sfxClips[(int)sfx];
	}
}
