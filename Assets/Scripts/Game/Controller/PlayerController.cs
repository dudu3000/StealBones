using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    public float health = 100f;

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
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            Move(input);

            if (Input.GetKey(KeyCode.Space) && !animator.GetBool("Jump")) {
                Jump();
            } else if (animator.GetBool("Jump")) {
                transform.Translate(new Vector3(input.x, 0, input.y) * speed * Time.deltaTime);
        }
    }

    protected override void Jump() {
        playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
        animator.SetBool("Jump", true);
        transform.Translate(new Vector3(input.x, 0, input.y) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            health -= 25f;

            if (health <= 0) {
                GameManager.gameRunning = false;
            }
        }
    }
}
