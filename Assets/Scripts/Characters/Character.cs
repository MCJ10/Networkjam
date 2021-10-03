using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Movement), typeof(SpriteRenderer), typeof(Animator))]
public class Character : MonoBehaviour
{
    readonly static CharAnimation[] animations =
        { CharAnimation.Normal, CharAnimation.Run, CharAnimation.JumpUp, CharAnimation.JumpDown };

    public bool invertedfFlipX;
    public Direction startDirection;


    Direction _direction;
    public Direction Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (Grounded && !Jumping && currentAnimation != CharAnimation.JumpUp)
            {
                if (Direction == Direction.Neutral)
                {
                    PlayAnimation(CharAnimation.Normal);
                    // animator.SetTrigger("Normal");
                    // Debug.Log($"Normal");
                }
                else
                {
                    PlayAnimation(CharAnimation.Run);
                    // animator.SetTrigger("Run");
                    // Debug.Log($"Run");
                }
            }

            if (_direction == value)
                return;

            // Debug.Log($"{_direction} to {value}");

            _direction = value;

            if (Direction == Direction.Left || Direction == Direction.Right)
            {
                spriteRenderer.flipX = Direction == Direction.Left;

                if (invertedfFlipX)
                    spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }


    public bool Grounded => CheckCollision(Vector3.down, 0f, .1f);// && Mathf.Abs(jump.rigidbody2D.velocity.y) < .01f ;
    public bool Jumping => jump != null && jump.rigidbody2D.velocity.y > .5f;


    [NonSerialized] public bool canMove;
    [NonSerialized] public Animator animator;
    protected Movement movement;
    protected Jump jump;
    protected CapsuleCollider2D cC2D;
    protected SpriteRenderer spriteRenderer;

    CharAnimation currentAnimation;


    protected void Awake()
    {
        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
        if (jump != null)
            jump.SetDefault();
        cC2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        Direction = startDirection;
        currentAnimation = CharAnimation.Normal;
    }

    protected bool CheckCollision(Vector3 direction, float raycastStartDistance, float raycastLength)
    {
        Vector2 origin = transform.position + (direction * raycastStartDistance);
        // Debug.Log($"({transform.position.x}, {transform.position.y}, {transform.position.z}) + ({direction} * {raycastStartDistance})");
        var hits = Physics2D.RaycastAll(origin, direction, raycastLength);

        Debug.DrawRay(origin, direction * raycastLength, Color.red);

        foreach(var hit in hits)
        {
            if (hit.collider.gameObject != gameObject)
            {
                // Debug.Log(hit.collider.name);
                return true;
            }
        }

        return false;
    }

    protected void Update()
    {
        if (jump != null && jump.rigidbody2D.velocity.y < -.5f)
        {
            PlayAnimation(CharAnimation.JumpDown);
        }
    }

    public void PlayAnimation(CharAnimation newAnimation)
    {
        currentAnimation = newAnimation;

        foreach(var animation in animations)
        {
            if (newAnimation != animation)
                animator.ResetTrigger(animation.ToString());
        }

        animator.SetTrigger(newAnimation.ToString());
    }
}
