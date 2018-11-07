using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactercontroller : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public int extraJumps = 1;

    public int extraDashes = 1;

    public float dashDistance = 2.5f;

    private int timesDashed = 0;
    private int timesJumped = 0;

    private Vector2 dashDirection;
    private bool doDash = false;

	[SerializeField]		
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    //private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        //spriteRenderer = GetComponent<SpriteRenderer> (); 
        //animator = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        //reset jumps
        if (grounded)
        {
            timesJumped = 0;
            timesDashed = 0;
        }


        if (Input.GetButtonDown ("Jump") &&  (grounded || timesJumped < extraJumps)) {
            if (!grounded) timesJumped++;
            velocity.y = jumpTakeOffSpeed;
        }

        if (Input.GetButtonDown("Fire1") && timesDashed < extraDashes)
        {
            doDash = true;
            dashDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized;
            timesDashed++;
        }

        //sustain midflight
        if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (move.x > 0) spriteRenderer.flipX = false;
        if (move.x < 0) spriteRenderer.flipX = true;

        if (move.x != 0 && grounded)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        targetVelocity = move * maxSpeed;
    }

    void FixedUpdate() {
        if (doDash) //modified for teleport.
        {
            doDash = false;
            finalPos = rb2d.position;
            velocity = Vector2.zero;

            Vector2 move = dashDirection * dashDistance;
            grounded = false;

            Movement(move);

            rb2d.MovePosition(finalPos);
        }
        else
        {
            base.FixedUpdate();
        }
    }
}

