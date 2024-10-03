using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : BaseWeaponController
{
    void Start()
    {
        isFiring = false;
        reloadTime = 3f;
        reloading = 5f;
        bulletSpawner = GameObject.Find("BulletSpawner");
        aimLookAt = GameObject.Find("AimLookAt");
    }
    void Update()
    {
        bulletSpawner.transform.LookAt(aimLookAt.transform);
        if (isFiring && reloading > reloadTime) {
            Firing();
            reloading = 0f;
        } else {
            reloading += Time.deltaTime;
        }
    }
}
