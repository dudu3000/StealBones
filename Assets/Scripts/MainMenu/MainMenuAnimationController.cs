using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationController : MonoBehaviour
{
    private Animator characterAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        characterAnimator.SetBool("idle1", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
