using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public Transform playerBody;

    private float mouseSensitivity = 800f;
    private float yRotation = 0f;
    private Rigidbody rbPlayer;
    private float jumpForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 15;
        Cursor.lockState = CursorLockMode.Locked;
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Move(verticalInput, horizontalInput);
        MouseController();
        Jump();
    }

    private void MouseController() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseX;
        yRotation = Mathf.Clamp(-90f, yRotation, 90f);
        transform.localRotation = Quaternion.Euler(0, -yRotation, 0f);
        playerBody.Rotate(Vector3.right * mouseY);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
