using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem effect;
    public AudioClip destroySound;

    private Vector3 spawnPosition;
    private float maxTravelDistance = 100f;
    private AudioSource bulletAS;

    void Start() {
        spawnPosition = transform.position;
        bulletAS = GetComponent<AudioSource>();
        bulletAS.enabled = true;
    }

    void Update() {
        if (!GameManager.Instance.gamePaused) {
            if (Vector3.Distance(spawnPosition, transform.position) >= maxTravelDistance) {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator playBulletSound() {
        bulletAS.PlayOneShot(destroySound);
        ParticleSystem particles = Instantiate(effect, transform.position, transform.rotation);
        particles.Play();

        while (bulletAS.isPlaying)
        {
            yield return null;
        }

        DestroyBullet(particles);
    }

    private void DestroyBullet(ParticleSystem particles) {
            Destroy(gameObject);
            Destroy(particles.gameObject, 1f);
    }

    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Bullet")) {
            StartCoroutine(playBulletSound());
        }
    }
}
