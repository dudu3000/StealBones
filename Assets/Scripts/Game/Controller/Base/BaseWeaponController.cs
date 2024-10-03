using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponController : WeaponControllerAbstract
{
    public ParticleSystem gunSmoke;
    public GameObject bullet;
    public GameObject aimLookAt;
    protected int numberOfBullets = 8;
    protected GameObject bulletSpawner;
    protected List<Vector3> bulletSpawnPositionsOffset = new List<Vector3> {
            new Vector3(0, 0.02f, 0),
            new Vector3(0, -0.02f, 0),
            new Vector3(0.02f, 0, 0),
            new Vector3(-0.02f, 0, 0),
            new Vector3(0, 0.04f, 0),
            new Vector3(0, -0.04f, 0),
            new Vector3(0.04f, 0, 0),
            new Vector3(-0.04f, 0, 0)
        };
    protected List<Vector3> bulletDirectionOffset = new List<Vector3> {
            new Vector3(0, 0.2f, 0),
            new Vector3(0, -0.2f, 0),
            new Vector3(0.2f, 0, 0),
            new Vector3(-0.2f, 0, 0),
            new Vector3(0, 0.04f, 0),
            new Vector3(0, -0.04f, 0),
            new Vector3(0.04f, 0, 0),
            new Vector3(-0.04f, 0, 0)
        };

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

    protected void SpawnBullets() {
        for (int i = 0; i < numberOfBullets; i++) {
            aimLookAt = GameObject.Find("AimLookAt");
            bulletSpawner = GameObject.Find("BulletSpawner");
            GameObject bulletSpawned = Instantiate(bullet, bulletSpawner.transform.position + bulletSpawnPositionsOffset[i], Quaternion.identity);
            bulletSpawned.GetComponent<Rigidbody>().AddForce(aimLookAt.transform.position - bulletSpawned.transform.position + bulletDirectionOffset[i], ForceMode.Impulse);
        }
    }
}
