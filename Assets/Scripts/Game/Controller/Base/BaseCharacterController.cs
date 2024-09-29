using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterControllerAbstract
{
    private float m_speed = 10;
    public float speed {
        get { return m_speed; }
        set {
            if (value > 0) {
                m_speed = value;
            } else {
                Debug.LogError("Speed must be greater than 0");
            }
        }
    }
    
    private bool m_isOnGround = false;
    public bool isOnGround {
        get { return m_isOnGround; }
        private set { m_isOnGround = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move(float vertical, float horizontal)
    {
        transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * m_speed);
    }

    protected override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor")) {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Floor")) {
            isOnGround = false;
        }
    }
}
