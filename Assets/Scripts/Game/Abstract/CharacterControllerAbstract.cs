using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerAbstract : MonoBehaviour
{
    protected abstract void Move(Vector2 input);
    protected abstract void Jump();
}
