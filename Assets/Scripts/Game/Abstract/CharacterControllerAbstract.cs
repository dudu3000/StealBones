using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerAbstract : MonoBehaviour
{
    protected abstract void Move(float vertical, float horizontal);
    protected abstract void Shoot();
}
