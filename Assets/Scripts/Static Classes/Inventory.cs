using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    private int wood = 0;
    private int fluff = 0;
    private int candy = 0;

    private void Start() {
        instance = this;
    }

}
