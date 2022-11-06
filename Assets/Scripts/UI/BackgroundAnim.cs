using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnim : MonoBehaviour {
    public Image img;

    public List<Sprite> backgrounds = new List<Sprite>();
    public float minChangeTime, maxChangeTime;

    private void Start() {
        StartCoroutine(bgChange());
    }

    IEnumerator bgChange() {
        yield return new WaitForSeconds(Random.Range(minChangeTime, maxChangeTime));
        int i = Random.Range(0, backgrounds.Count);
        img.sprite = backgrounds[i];
        StartCoroutine(bgChange());
    }

}
