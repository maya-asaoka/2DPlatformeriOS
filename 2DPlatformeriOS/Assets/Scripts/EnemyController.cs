using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public float velocity = 0.02f;

    protected bool isMovingLeft;

    protected Rigidbody2D rb2d;
    protected SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        isMovingLeft = true;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDistance = Vector2.zero;
        if (isMovingLeft)
        {
            moveDistance = new Vector2(-velocity, 0f);
        }
        else
        {
            moveDistance = new Vector2(velocity, 0f);
        }
        rb2d.position = rb2d.position + moveDistance;
    }

    // switch directions at edge of ground
    protected abstract void OnTriggerEnter2D(Collider2D collision);

}
