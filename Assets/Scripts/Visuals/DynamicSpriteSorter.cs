using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpriteSorter : MonoBehaviour
{
    public SpriteRenderer sprt;

    void Update()
    {
        sprt.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
