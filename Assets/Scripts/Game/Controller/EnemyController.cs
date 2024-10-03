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
    
    private float speed = 3.5f;
    private PlayerInRange playerDetection;
    private WeaponEnemyController weaponController;
    private EnemyCharacterAiming aimingController;
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
        if (playerDetection.playerInRange) {
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
}
