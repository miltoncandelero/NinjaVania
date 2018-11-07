using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactercontroller : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public int extraJumps = 1;
    private int timesJumped = 0;

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
        if (grounded) timesJumped = 0;


        if (Input.GetButtonDown ("Jump") &&  (grounded || timesJumped < extraJumps)) {
            if (!grounded) timesJumped++;
            velocity.y = jumpTakeOffSpeed;
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
}

