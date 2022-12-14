using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer sprt;
    private Rigidbody2D rb;

    private float xVel, yVel;
    private bool canSleep;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        xVel = Input.GetAxisRaw("Horizontal");
        yVel = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z) && canSleep) {
            GameState.instance.ToggleDay();
        }
    }
    private void FixedUpdate() {
        Vector2 normalized = new Vector2(xVel, yVel);
        normalized = Vector3.Normalize(normalized);
        rb.velocity = normalized * moveSpeed;
        anim.SetFloat("xVel", Mathf.Abs(rb.velocity.x));
        if(xVel > 0) {
            sprt.flipX = false;
        } else if(xVel < 0) {
            sprt.flipX = true;
        }
        anim.SetFloat("yVel", rb.velocity.y);
    }

    public void SetCanSleep(bool canSleep) {
        this.canSleep = canSleep;
	}

    public void GoToBed() {
        sprt.enabled = false;
        canSleep = false;
        //gameObject.SetActive(false);
    }

    public void WakeUp() {
        Invoke(nameof(ShowPlayer), 0.67f);
    }

    private void ShowPlayer() {
        sprt.enabled = true;
        //gameObject.SetActive(true);
    }
}
