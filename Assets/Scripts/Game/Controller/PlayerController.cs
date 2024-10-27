using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

// INHERITANCE
public class PlayerController : BaseCharacterController
{
    public float health = 100f;
    public GameObject gameMenu;
    public GameObject endMenu;
    public GameObject finishMenu;
    public GameObject optionsMenu;
    public GameObject finishMenuText;
    public ParticleSystem shootBloodEffect;
    public ParticleSystem deadBloodEffect;

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
        endMenu.SetActive(false);
        finishMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameFinished) {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                Destroy(enemy);
            }
            finishMenu.SetActive(true);
            finishMenuText.GetComponent<TextMeshProUGUI>().text = $"{(GameManager.Instance.bestTimeReadInSeconds > GameManager.Instance.currentRunTimeInSeconds ? "Congrats!\n" : "")}{(GameManager.Instance.bestTimeReadInSeconds > GameManager.Instance.currentRunTimeInSeconds ? "You are the new leader!\n" : "")}\nBest time: {GameManager.Instance.bestTimeRead}\nYour time: {GameManager.Instance.bestTime}";
            GameManager.Instance.gamePaused = true;
            return;
        }
        if (!GameManager.Instance.gamePaused && GameManager.gameRunning) {
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
        } else if (GameManager.Instance.gamePaused) {
            Move(new Vector2(0, 0));
            if (!optionsMenu.activeSelf) {
                gameMenu.SetActive(true);
            }
        }
    }

    // POLYMORPHISM
    protected override void Jump() {
        playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
        animator.SetBool("Jump", true);
        transform.Translate(new Vector3(input.x, 0, input.y) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            health -= 5f;
            ParticleSystem shootBlood = Instantiate(shootBloodEffect, transform.position, transform.rotation);
            shootBlood.Play();

            if (health <= 0) {
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                    Destroy(enemy);
                }
                ParticleSystem deadBlood = Instantiate(deadBloodEffect, transform.position, transform.rotation);
                deadBlood.Play();
                GameManager.gameRunning = false;
                endMenu.SetActive(true);
                Destroy(gameObject);
                return;
            }
        }
    }
}
