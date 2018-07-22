﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : EnemyController {
    
    // switch directions at edge of ground
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            // change directions and flip sprite
            isMovingLeft = !isMovingLeft;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
