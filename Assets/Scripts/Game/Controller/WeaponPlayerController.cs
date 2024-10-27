using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerController : BaseWeaponController
{
    public GameObject aimLookAt;
    public GameObject bulletSpawner;
    void Start()
    {
        isFiring = false;
        reloadTime = 3f;
        reloading = 5f;
    }

    void Update()
    {
        if (!GameManager.Instance.gamePaused) {
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
