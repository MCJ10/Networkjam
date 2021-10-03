using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Movement))]
public class Player : Character
{
    public Sprite dieSprite;

    new void Awake()
    {
        base.Awake();
        jump = GetComponent<Jump>();
        canMove = true;
    }

    new void Update()
    {
        if (!canMove)
        {
            return;
        }

        base.Update();

        if (Input.GetKey(KeyCode.A))
        {
            Direction = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Direction = Direction.Right;
        }
        else
        {
            Direction = Direction.Neutral;
        }
    }

    void FixedUpdate()
    {
        movement.Move(Direction);
    }

    public void Die()
    {
        canMove = false;
        jump.rigidbody2D.velocity = Vector2.zero;
        animator.enabled = false;
        spriteRenderer.sprite = dieSprite;
    }
}
