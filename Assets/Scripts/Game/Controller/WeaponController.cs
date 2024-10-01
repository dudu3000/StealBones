using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : WeaponControllerAbstract
{
    public ParticleSystem gunSmoke;
    public GameObject bullet;
    public GameObject aimLookAt;
    private int numberOfBullets = 9;
    private GameObject bulletSpawner;

    void Start()
    {
        isFiring = false;
        reloadTime = 5f;
        reloading = 0f;
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
    public override void StartFiring() {
        isFiring = true;
    }

    public override void StopFiring()
    {
        isFiring = false;
    }

    public override void Firing() {
        gunSmoke.Play();
        isFiring = false;
        SpawnBullets();
    }

    private void SpawnBullets() {
        for (int i = 0; i < numberOfBullets; i++) {
            aimLookAt = GameObject.Find("AimLookAt");
            bulletSpawner = GameObject.Find("BulletSpawner");
            GameObject bulletSpawned = Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
            bulletSpawned.GetComponent<Rigidbody>().AddForce(aimLookAt.transform.position - bulletSpawner.transform.position, ForceMode.Impulse);
        }
    }
}
