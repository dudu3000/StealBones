using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// INHERITANCE
public class BaseWeaponController : WeaponControllerAbstract
{
    public ParticleSystem gunSmoke;
    public GameObject bullet;
    protected int numberOfBullets = 8;
    public AudioSource gunAS;
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

    // POLYMORPHISM
    public override void StartFiring() {
        isFiring = true;
    }

    // POLYMORPHISM
    public override void StopFiring()
    {
        isFiring = false;
    }

    // POLYMORPHISM
    public override void Firing(GameObject aimLookAt, GameObject bulletSpawner) {
        gunSmoke.Play();
        gunAS.PlayOneShot(gunAS.clip);
        isFiring = false;
        SpawnBullets(aimLookAt, bulletSpawner);
    }

    protected void SpawnBullets(GameObject aimLookAt, GameObject bulletSpawner) {
        for (int i = 0; i < numberOfBullets; i++) {
            GameObject bulletSpawned = Instantiate(bullet, bulletSpawner.transform.position + bulletSpawnPositionsOffset[i], Quaternion.identity);
            bulletSpawned.GetComponent<Rigidbody>().AddForce((aimLookAt.transform.position - bulletSpawned.transform.position + bulletDirectionOffset[i])*5, ForceMode.Impulse);
        }
    }
}
