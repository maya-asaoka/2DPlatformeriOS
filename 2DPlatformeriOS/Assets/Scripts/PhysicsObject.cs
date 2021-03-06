﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float minNormalGround = 0.65f;
    public float gravityModifier = 1.25f;

    protected bool grounded;
    protected Vector2 normalGround;

    protected Vector2 targetVelocity;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

    protected const float minMoveDist = 0.001f;
    protected const float shellRad = 0.01f;

    // only called if the object is active (can be called more than once)
    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Use this for initialization, called only once per object
    void Start () 
    {
        // contact filter sets what the object will collide with
        contactFilter.useTriggers = false;
        // use layer collision matrix settings (in physics 2d settings)
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity ();
	}

    protected virtual void ComputeVelocity() {}

    // called more frequently than update
    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(normalGround.y, -normalGround.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement (move, false);

        move = Vector2.up * deltaPosition.y;

        Movement (move, true);
    }

    void Movement(Vector2 move, bool yMovement) 
    {
        float distance = move.magnitude;

        if (distance > minMoveDist) 
        {
            // shellRad so object never gets stuck inside another collider
            // cast: casting collider forwards by "move" to see if it'll hit another collider
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRad);
            hitBufferList.Clear ();

            for (int i = 0; i < count; i++) 
            {
                hitBufferList.Add (hitBuffer [i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList [i].normal;

                // check if ground is flat enough to stand on
                if (currentNormal.y > minNormalGround)
                {
                    grounded = true;

                    if (yMovement)
                    {
                        normalGround = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                // slow down movement if colliding non-orthogonally
                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                // if conditional is true, use modified, else use distance
                // ensures it doesn't go too far and get stuck in another collider
                float modifiedDistance = hitBufferList [i].distance - shellRad;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}
