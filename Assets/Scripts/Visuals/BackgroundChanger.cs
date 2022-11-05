using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour {
    public SpriteRenderer sprt;

    public Sprite daySprite;
    public Sprite nightSprite;

    private void Update() {
        if(GameState.instance.IsDay) {
            sprt.sprite = daySprite;
        } else {
            sprt.sprite = nightSprite;
        }
    }

}
