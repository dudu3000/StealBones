using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyController : BaseCharacterController
{
    public GameObject spine1;
    public GameObject spine2;
    public GameObject head;
    public GameObject weaponPose;
    public float health = 100f;
    public ParticleSystem destroyEffect;
    public float initialRunInSeconds;

    private float zRunning = 1;
    private float speed = 3.5f;
    private PlayerInRange playerDetection;
    private WeaponEnemyController weaponController;
    private EnemyCharacterAiming aimingController;
    private float runingTimePassed = 0f;
    private bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerDetection = GetComponentInChildren<PlayerInRange>();
        weaponController = GetComponentInChildren<WeaponEnemyController>();
        aimingController = GetComponent<EnemyCharacterAiming>();
        spine1.GetComponent<MultiAimConstraint>().weight = 0;
        spine2.GetComponent<MultiAimConstraint>().weight = 0;
        head.GetComponent<MultiAimConstraint>().weight = 0;
        weaponPose.GetComponent<MultiAimConstraint>().weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        runingTimePassed += Time.deltaTime;
        if (runingTimePassed < initialRunInSeconds) {
            Move(new Vector2(0, zRunning));
        } else {
            Move(new Vector2(0, 0));
            isRunning = false;
        }
        if (playerDetection.playerInRange && !isRunning) {
            weaponController.playerInRange = true;
            aimingController.playerInRange = true;
            spine1.GetComponent<MultiAimConstraint>().weight = 1;
            spine2.GetComponent<MultiAimConstraint>().weight = 1;
            head.GetComponent<MultiAimConstraint>().weight = 1;
            weaponPose.GetComponent<MultiAimConstraint>().weight = 1;
        } else {
            weaponController.playerInRange = false;
            aimingController.playerInRange = false;
            spine1.GetComponent<MultiAimConstraint>().weight = 0;
            spine2.GetComponent<MultiAimConstraint>().weight = 0;
            head.GetComponent<MultiAimConstraint>().weight = 0;
            weaponPose.GetComponent<MultiAimConstraint>().weight = 0;
        }
    }

    private void Run() {

    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            health -= 12f;

            if (health <= 0) {
                ParticleSystem destroyEffectApplied = Instantiate(destroyEffect, other.gameObject.transform.position, transform.rotation);
                destroyEffectApplied.Play();

                Destroy(gameObject);
                Destroy(destroyEffectApplied.gameObject, 1f);
            }
        }
    }
}
