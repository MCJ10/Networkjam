using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    public float jump;


    [NonSerialized] public new Rigidbody2D rigidbody2D;
    CapsuleCollider2D capsuleCollider2D;
    Character character;

    public void SetDefault()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        character = GetComponent<Character>();
    }

    void Update()
    {
        if (character.canMove && Input.GetKeyDown(KeyCode.Space) && character.Grounded)
        {
            rigidbody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            character.PlayAnimation(CharAnimation.JumpUp);
        }
    }
}
