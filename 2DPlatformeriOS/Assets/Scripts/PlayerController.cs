using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public bool isDead;
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;
    public Vector3 respawnPoint;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
	// Use this for initialization
	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
	}

    protected override void ComputeVelocity()
    {
        if (isDead)
        {
            return;
        }

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) 
        {
            velocity.y = jumpTakeOffSpeed;
        }

        // cancel jump in midair if jump button released
        else if (Input.GetButtonUp ("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

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

    // to see if player has entered the trigger (ie. the fall detector) or a checkpoint
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
    }

}
