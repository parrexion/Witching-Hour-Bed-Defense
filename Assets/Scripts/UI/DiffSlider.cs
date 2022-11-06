using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiffSlider : MonoBehaviour {

    public TextMeshProUGUI txt;

    public List<string> names = new List<string>();
    public List<Color> colors = new List<Color>();
    public List<float> mult = new List<float>();

    public GameObject rightBtn;
    public GameObject leftBtn;

    private int i = 2;

    private void Start() {
        txt.color = colors[i];
        txt.text = names[i];
        Difficulty.diffMult = mult[i];
    }

    public void right() {
        i++;
        if(i == names.Count-1) {
            rightBtn.SetActive(false);
        }
        leftBtn.SetActive(true);
        txt.color = colors[i];
        txt.text = names[i];
        Difficulty.diffMult = mult[i];
    }
    public void left() {
        i--;
        if(i == 0) {
            leftBtn.SetActive(false);
        }
        rightBtn.SetActive(true);
        txt.color = colors[i];
        txt.text = names[i];
        Difficulty.diffMult = mult[i];
    }

}
