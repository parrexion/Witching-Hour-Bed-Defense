using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCanvas : MonoBehaviour {

    public TextMeshProUGUI woodAmountLabel;
    public TextMeshProUGUI fluffAmountLabel;
    public TextMeshProUGUI candyAmountLabel;
    public TextMeshProUGUI healthAmountLabel;

    public static InventoryCanvas instance;

    // Start is called before the first frame update
    void Start() {
        instance = this;
        setText();
    }

    public static void updateText() {
        if (instance != null) {
            instance.setText();
        }
    }

    private void setText() {
        if (Inventory.instance != null) {
            woodAmountLabel.text = Inventory.instance.getWood().ToString();
            fluffAmountLabel.text = Inventory.instance.getFluff().ToString();
            candyAmountLabel.text = Inventory.instance.getCandy().ToString();

            healthAmountLabel.text = (Inventory.instance.getHealth() / Inventory.instance.maxHealth * 100).ToString() + "%";
        }
    }
}
