using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderRange : MonoBehaviour {

        public ExploderTower exploder;
        
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            exploder.explode();
        }
    }

}
