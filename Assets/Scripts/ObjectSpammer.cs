using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpammer : MonoBehaviour
{
    public GameObject prefab;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
