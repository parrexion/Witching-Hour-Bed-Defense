using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D rb;

    private float xVel, yVel;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        xVel = Input.GetAxisRaw("Horizontal");
        yVel = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate() {
        Vector2 normalized = new Vector2(xVel, yVel);
        normalized = Vector3.Normalize(normalized);
        rb.velocity = normalized * moveSpeed;
    }
}
