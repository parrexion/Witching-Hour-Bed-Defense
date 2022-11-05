using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour {
    public static ChatBubble instance;
    private void Start() {
        instance = this;
        show();
    }

    public TextMeshProUGUI txt;
    public float timeBetweenCharacters;
    public List<GameObject> childObjects = new List<GameObject>();

    private Coroutine lastRoutine;
    private int i;

    private void show() {
        foreach(GameObject child in childObjects) {
            child.SetActive(true);
        }
    }

    public void displayMessage(string message) {
        if(lastRoutine != null) {
            StopCoroutine(lastRoutine);
            lastRoutine = null;
        }
        txt.text = "";
        lastRoutine = StartCoroutine(anotherChar(message, 0));
    }

    IEnumerator anotherChar(string message, int i) {
        txt.text += message[i];
        i++;
        yield return new WaitForSeconds(timeBetweenCharacters);
        if(message.Length > i) {
            lastRoutine = StartCoroutine(anotherChar(message, i));
        } else {
            lastRoutine = null;
            Invoke("hide", 2.5f);
        }
    }
    private void hide() {
        foreach(GameObject child in childObjects) {
            child.SetActive(false);
        }
    }

}
