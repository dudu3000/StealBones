using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterAiming : BaseCharacterAiming
{
    private WeaponEnemyController weapon;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<WeaponEnemyController>();
        player = GameObject.Find("TargetLookAt");
    }

    protected void LateUpdate() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            weapon.StartFiring();
        } else {
            weapon.StopFiring();
        }
    }
    protected override void FixedUpdate() {
        transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
    }
}