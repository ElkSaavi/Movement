using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stance : CharacterController
{
    private int layerMask = (1 << 20) | (1 << 21);
    private float groundCheckDistance;


    private void FixedUpdate()
    {
        CheckGrounding();
        CheckBearing();
        CheckSlope();
    }
    private void CheckGrounding()
    {
        if (rb.velocity.y > 0)
            groundCheckDistance = 1.65f;
        else
            groundCheckDistance = -rb.velocity.y * Time.fixedDeltaTime + 1.65f;

        if (Physics.Raycast(tf.position, Vector3.down, out groundCheck, groundCheckDistance, layerMask))
        {
            if (!grounded)
            {
                impact = true;
                impactSpeed = Mathf.Abs(rb.velocity.y);
            }
            else
                impact = false;

            grounded = true;
        }
    }
    private void CheckBearing()
    {
        forward = Vector3.Cross(groundCheck.normal, tf.right);
        side = Vector3.Cross(tf.forward, groundCheck.normal);
    }
    private void CheckSlope()
    {
        if (grounded)
            slope = Vector3.Angle(rb.velocity, Vector3.down);
        else
            slope = 90;
    }
}

