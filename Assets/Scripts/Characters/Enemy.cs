using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Vector3 collisionDirection;
    float halfWidth;

    float lastPosX;

    void Start()
    {
        halfWidth = cC2D.size.x / 2f;
        float halfHeight = cC2D.size.y / 2f;

        collisionDirection = new Vector3(0, -.05f);
    }

    new void Update()
    {
        collisionDirection.x = Direction == Direction.Left? -halfWidth : halfWidth;

        if (!CheckCollision(collisionDirection, 1.5f, .1f))
        {
            Direction = Direction == Direction.Left? Direction.Right : Direction.Left;
            movement.Move(Direction);
        }
    }

    void FixedUpdate()
    {
        movement.Move(Direction);
    }
}
