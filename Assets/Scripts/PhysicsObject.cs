using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

[SerializeField]
    protected float minMoveDistance = 0.001f;

[SerializeField]
    protected float shellRadius = 0.01f;

    protected Vector2 finalPos;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void Start () 
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    
    void Update () 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity (); 
    }

    protected virtual void ComputeVelocity()
    {
    
    }

    protected void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Debug.DrawRay(rb2d.position,moveAlongGround * 3,Color.magenta);
        Debug.DrawRay(rb2d.position,groundNormal * 3,Color.yellow);

        finalPos = rb2d.position;

        Movement (move);

        move = Vector2.up * deltaPosition.y;

        Movement (move);

        if (!grounded) groundNormal = Vector2.up;

        rb2d.MovePosition(finalPos);
    }

    protected void Movement(Vector2 move)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance) 
        {
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear ();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add (hitBuffer [i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList [i].normal;
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;
                    groundNormal = currentNormal;
                    currentNormal.x = 0;
                }

                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        //rb2d.position = rb2d.position + move.normalized * distance;
        //rb2d.MovePosition(rb2d.position + move.normalized * distance;

        finalPos = finalPos + move.normalized * distance;
    }

}