using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Movement : MonoBehaviour
{
    public float speed;
    public float airSpeedPercentage;


    new Rigidbody2D rigidbody2D;
    Jump jump;
    Character character;


    float maxLeftSpeed => -speed * Time.fixedDeltaTime;
    float maxRightSpeed => speed * Time.fixedDeltaTime;


    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        jump = GetComponent<Jump>();
        character = GetComponent<Character>();
    }

    public void Move(Direction direction)
    {
        if (!character.Grounded && direction == Direction.Neutral)
            return;

        float xVelocity = 0f;

        if (character.Grounded)
        {
            xVelocity = direction == Direction.Left? maxLeftSpeed :
                direction == Direction.Right? maxRightSpeed : 0f;
        }
        else
        {
            float speedPercentage = direction == Direction.Left? -airSpeedPercentage : airSpeedPercentage;
            xVelocity = rigidbody2D.velocity.x + (speedPercentage * maxRightSpeed);

            if (xVelocity <= maxLeftSpeed || xVelocity >= maxRightSpeed)
                return;
        }

        // Debug.Log(xVelocity);
        rigidbody2D.velocity = new Vector2(xVelocity, rigidbody2D.velocity.y);
    }
}
