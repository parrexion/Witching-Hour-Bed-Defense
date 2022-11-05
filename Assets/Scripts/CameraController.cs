using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private Transform player, bed;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float followCamSize, bedCamSize;


    private bool followPlayer = true;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            toggle();
        }
        if(followPlayer) {
            transform.position = player.position + offset;
        } else {
            transform.position = bed.position + offset;
        }
    }
    private void toggle() {
        followPlayer = !followPlayer;
        if(followPlayer) {
            StartCoroutine(zoomIn());
        } else {
            StartCoroutine(zoomOut());
        }
    }

    IEnumerator zoomOut() {
        yield return new WaitForSeconds(0.01f);
        cam.orthographicSize += 0.02f;
        if(cam.orthographicSize < bedCamSize) {
            StartCoroutine(zoomOut());
        } else {
            cam.orthographicSize = bedCamSize;
        }
    }

    IEnumerator zoomIn() {
        yield return new WaitForSeconds(0.01f);
        cam.orthographicSize -= 0.02f;
        if(cam.orthographicSize > followCamSize) {
            StartCoroutine(zoomIn());
        } else {
            cam.orthographicSize = followCamSize;
        }
    }

}
