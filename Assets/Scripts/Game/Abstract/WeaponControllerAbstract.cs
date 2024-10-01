using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponControllerAbstract : MonoBehaviour
{
    private bool m_isFiring;
    public bool isFiring {
        get { return m_isFiring; }
        set { m_isFiring = value; }
    }
    private float m_reloadTime;
    public float reloadTime {
        get { return m_reloadTime; }
        set { m_reloadTime = value; }
    }
    private float m_reloading;
    public float reloading {
        get { return m_reloading; }
        set { m_reloading = value; }
    }
    public abstract void StartFiring();
    public abstract void StopFiring();
    public abstract void Firing();
}
