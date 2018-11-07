using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    bool grounded;
    private int timesJumped;

    public float xSpeed;
    public int jumps;
    public float jumpForce;
    public float secondJumpForce;

    public float gravityUp;
    public float gravityDefault;
    public float gravityDown;

    public LayerMask solids;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
	}


    bool IsGrounded()
    {
        return (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, solids));

    }


    // Update is called once per frame
    void Update () {

        //x move
        if (Input.GetAxis("Horizontal") > 0) sr.flipX = false;
        if (Input.GetAxis("Horizontal") < 0) sr.flipX = true;

        //flip
        if (Input.GetAxis("Horizontal")!= 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        //move X
        rb.velocity = new Vector2(xSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        

        //jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Debug.Log("Jump");
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                timesJumped = 1;
            }
            else if (timesJumped < jumps)
            {
                Debug.Log("Double Jump!");
                rb.velocity = rb.velocity * Vector2.right; //kill downward movement.
                rb.AddForce(new Vector2(0, secondJumpForce), ForceMode2D.Impulse);
                timesJumped++;
            }


        }

        //alter physics.
        if (rb.velocity.y < 0)
            rb.gravityScale = gravityDown;
        else if (rb.velocity.y > 1 && Input.GetButton("Jump")) rb.gravityScale = gravityUp; else rb.gravityScale = gravityDefault;
    }
}
