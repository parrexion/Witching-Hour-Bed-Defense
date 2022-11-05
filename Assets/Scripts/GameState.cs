using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    private static GameState instance;

    private bool inBuildMode = false;

    private void Start() {
        instance = this;
    }

    public void toggleBuildMode() {
        inBuildMode = !inBuildMode;
    }

    public bool isInbuildMode() {
        return inBuildMode;
    }

}
