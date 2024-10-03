using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem effect;
    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Bullet")) {
            ParticleSystem particles = Instantiate(effect, transform.position, transform.rotation);
            particles.Play();
            Destroy(gameObject);
        }
    }
}
