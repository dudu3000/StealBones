using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemyController : BaseWeaponController
{
    public GameObject aimLookAt;
    public GameObject bulletSpawner;
    public bool playerInRange = false;
    void Start()
    {
        isFiring = false;
        reloadTime = 3f;
        reloading = 5f;
    }
    
    void Update()
    {
        if (playerInRange) {
            bulletSpawner.transform.LookAt(aimLookAt.transform);
            if (isFiring && reloading > reloadTime) {
                Firing(aimLookAt, bulletSpawner);
                reloading = 0f;
            } else {
                reloading += Time.deltaTime;
            }
        }
    }
}
