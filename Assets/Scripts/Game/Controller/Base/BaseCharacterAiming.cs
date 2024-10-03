using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    protected Camera mainCamera;

    protected virtual void FixedUpdate() {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}
