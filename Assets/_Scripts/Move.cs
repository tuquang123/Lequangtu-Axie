using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    //public Animator anm;

    private Vector2 movement;
    private static readonly int Run = Animator.StringToHash("run");
    bool facingRight;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        if (movement.x < 0 && facingRight)
        {
            Flip();
        }
        //anm.SetBool(Run, movement != Vector2.zero);
    }

    void Flip()
    {
        var o = gameObject;
        Vector3 currentScale = o.transform.localScale;
        currentScale.x *= -1;
        o.transform.localScale = currentScale;

        facingRight = !facingRight;

    }
}
