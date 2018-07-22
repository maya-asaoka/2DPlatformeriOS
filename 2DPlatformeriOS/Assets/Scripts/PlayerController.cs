﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public bool isDead;
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    public Vector3 respawnPoint;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
	}

    // computes velocity based on player input
    protected override void ComputeVelocity()
    {
        if (isDead)
        {
            return;
        }

        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis ("Horizontal");

        // can only jump if grounded (no double jumps)
        if (Input.GetButtonDown ("Jump") && grounded) 
        {
            velocity.y = jumpTakeOffSpeed;
        }

        // slow jump in midair if jump button released (cancel jump)
        else if (Input.GetButtonUp ("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        // face sprite in appropriate direction for movement
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        // set x velocity
        targetVelocity = move * maxSpeed;
    }

    // checking if player collided with a fall detector, checkpoint (respawn point), or an enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDetector")
        {
            animator.SetBool("hurt", true);
            if (GameController.instance.PlayerHit() == true)
            {
                transform.position = respawnPoint;
                animator.SetBool("hurt", false);
            }
            else
            {
                isDead = true;
            }
        }
        if (other.tag == "Respawn")
        {
            respawnPoint = other.transform.position;
        }
        if (other.tag == "Enemy")
        {
            animator.SetBool("hurt", true);
            if (GameController.instance.PlayerHit() == true)
            {
                velocity.y = jumpTakeOffSpeed;
                animator.SetBool("hurt", false);
            }
            else
            {
                isDead = true;
            }
        }
    }

}
