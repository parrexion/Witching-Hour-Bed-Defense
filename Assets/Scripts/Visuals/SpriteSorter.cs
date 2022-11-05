using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{

    public SpriteRenderer sprt;
    public int offSet;

    private void Start() {
        sprt.sortingOrder = Mathf.RoundToInt(transform.position.y * 100) * -1 + offSet;
    }
}
