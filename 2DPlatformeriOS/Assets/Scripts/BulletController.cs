using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    
    public float velocity = 0.1f;

    private Rigidbody2D rb2d;
    private bool isMovingRight;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        if (PlayerController.instance.spriteRenderer.flipX)
        {
            isMovingRight = false;
        }
        else
        {
            isMovingRight = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isMovingRight)
        {
            rb2d.position = rb2d.position + new Vector2(velocity, 0f);
        }
        else
        {
            rb2d.position = rb2d.position - new Vector2(velocity, 0f);
        }
	}

    // if hits an enemy, destroy both bullet and enemy objects
    // bullets should also disappear at the edge of screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemySpawner.instance.currentEnemyCount--;
            GameObject enemy = collision.gameObject;
            Destroy(enemy);
            Destroy(gameObject);
        }
        if (collision.tag == "Respawn")
        {
            Destroy(gameObject);
        }
    }
}
