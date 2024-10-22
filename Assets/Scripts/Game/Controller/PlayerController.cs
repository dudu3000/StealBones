using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    public float health = 100f;
    public GameObject gameMenu;

    private float speed = 3.5f;
    private AudioSource playerAS;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAS = GetComponent<AudioSource>();
        playerAS.pitch = 4;
        GameManager.Instance.gamePaused = false;
        GameManager.gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gamePaused) {
            gameMenu.SetActive(false);
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (input.x != 0 || input.y != 0) {
                playerAS.enabled = true;
            } else {
                playerAS.enabled = false;
            }
            Move(input);

            if (Input.GetKey(KeyCode.Space) && !animator.GetBool("Jump")) {
                playerAS.enabled = false;
                Jump();
            } else if (animator.GetBool("Jump")) {
                playerAS.enabled = false;
                transform.Translate(new Vector3(input.x, 0, input.y) * speed * Time.deltaTime);
            }
        } else {
            Move(new Vector2(0, 0));
            gameMenu.SetActive(true);
        }
    }

    protected override void Jump() {
        playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
        animator.SetBool("Jump", true);
        transform.Translate(new Vector3(input.x, 0, input.y) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            health -= 5f;

            if (health <= 0) {
                GameManager.gameRunning = false;
            }
        }
    }
}
