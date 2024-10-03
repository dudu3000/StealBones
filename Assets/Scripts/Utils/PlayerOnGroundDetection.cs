using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnGroundDetection : MonoBehaviour
{
    private GameObject player;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            playerAnimator.SetBool("Jump", false);
        }
    }
}
