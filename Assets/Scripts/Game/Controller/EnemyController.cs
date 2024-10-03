using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    private float speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
