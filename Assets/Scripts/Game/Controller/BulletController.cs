using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem effect;

    private Vector3 spawnPosition;
    private float maxTravelDistance = 100f;

    void Start() {
        spawnPosition = transform.position;
    }

    void Update() {
        if (Vector3.Distance(spawnPosition, transform.position) >= maxTravelDistance) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Bullet")) {
            ParticleSystem particles = Instantiate(effect, transform.position, transform.rotation);
            particles.Play();
            Destroy(gameObject);
            Destroy(particles.gameObject, 1f);
        }
    }
}
