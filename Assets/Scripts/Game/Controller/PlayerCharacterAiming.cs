using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAiming : BaseCharacterAiming
{
    private WeaponPlayerController weapon;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<WeaponPlayerController>();
    }

    protected void LateUpdate() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            weapon.StartFiring();
        } else {
            weapon.StopFiring();
        }
    }
}
