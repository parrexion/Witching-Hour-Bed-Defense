using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {
    public SpriteRenderer sprt;

    public List<Sprite> eyes = new List<Sprite>();
    public float minChangeTime, maxChangeTime;

    private void Start() {
        StartCoroutine(eyeChange());
    }

    IEnumerator eyeChange() {
        yield return new WaitForSeconds(Random.Range(minChangeTime, maxChangeTime));
        int i = Random.Range(0, eyes.Count);
        sprt.sprite = eyes[i];
        StartCoroutine(eyeChange());
    }

}
