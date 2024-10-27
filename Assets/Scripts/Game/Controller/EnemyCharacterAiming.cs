using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class EnemyCharacterAiming : BaseCharacterAiming
{
    public bool playerInRange = false;
    
    private WeaponEnemyController weapon;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        weapon = GetComponentInChildren<WeaponEnemyController>();
        player = GameObject.Find("TargetLookAt");
    }

    protected void LateUpdate() {
        if (!GameManager.Instance.gamePaused) {
            if (playerInRange) {
                weapon.StartFiring();
            } else {
                weapon.StopFiring();
            }
        }
    }
    // POLYMORPHISM
    protected override void FixedUpdate() {
        if (!GameManager.Instance.gamePaused) {
            if (playerInRange && GameObject.Find("Player") != null) {
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }
        }
    }
}
