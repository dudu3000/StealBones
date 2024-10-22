using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AimPausedController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gamePaused) {
            GetComponent<CinemachineFreeLook>().enabled = false;
        } else {
            GetComponent<CinemachineFreeLook>().enabled = true;
        }
    }
}
