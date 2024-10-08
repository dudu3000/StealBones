using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject lid;
    public GameObject activeCircle;
    public ParticleSystem triggerOutsideCircle;
    public ParticleSystem openedEffect;

    private float openSpeed = 10f;
    private float openSpeed1 = 1f;
    private float maxOpeningAngle = -90;
    private bool isOpening = false;
    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Opening();
    }

    private void Opening() {
        if (isOpening && activeCircle.transform.localScale.x < 10) {
            Quaternion lidRotation = new Quaternion();
            lidRotation.eulerAngles = lid.transform.localEulerAngles;
            lidRotation.eulerAngles += new Vector3(maxOpeningAngle / openSpeed * Time.deltaTime, 0, 0);
            lid.transform.localRotation = lidRotation;

            activeCircle.transform.localScale += new Vector3(openSpeed1 * Time.deltaTime, 0, openSpeed1 * Time.deltaTime);
        } else if (!isOpening && activeCircle.transform.localScale.x > 0.5f && !isOpened) {
            Quaternion lidRotation = new Quaternion();
            lidRotation.eulerAngles = lid.transform.localEulerAngles;
            lidRotation.eulerAngles -= new Vector3(maxOpeningAngle / openSpeed * Time.deltaTime, 0, 0);
            lid.transform.localRotation = lidRotation;

            activeCircle.transform.localScale -= new Vector3(openSpeed1 * Time.deltaTime, 0, openSpeed1 * Time.deltaTime);
        }
        if (activeCircle.transform.localScale.x >= 10 && !isOpened) {
            isOpened = true;
            ParticleSystem openedEffectInstantiated = Instantiate(openedEffect, transform.position, new Quaternion());
            openedEffectInstantiated.Play();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isOpening = false;
        }
    }
}
