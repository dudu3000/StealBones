using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterControllerAbstract
{
    protected Animator animator;
    protected Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move(Vector2 input)
    {
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }

    protected override void Jump()
    {
        playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
        animator.SetBool("Jump", true);
    }
}
