using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// INHERITANCE
public class BaseCharacterController : CharacterControllerAbstract
{
    protected Animator animator;
    protected Rigidbody playerRb;
    protected Vector2 input;
    // POLYMORPHISM
    protected override void Move(Vector2 input)
    {
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }

    // POLYMORPHISM
    protected override void Jump()
    {
        playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
        animator.SetBool("Jump", true);
    }
}
