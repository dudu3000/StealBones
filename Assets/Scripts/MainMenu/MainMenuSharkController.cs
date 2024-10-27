using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSharkController : MonoBehaviour
{
    private float speed = 5f;
    private Vector3 initialPosition = new Vector3(-4, -0.5f, -4.8f);
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.x > 50 || transform.position.x < -50) {
            transform.Rotate(Vector3.up * 180);
        }
    }
}
