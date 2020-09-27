using System;
using System.Collections;
using System.Collections.Generic;
using MovementAndChars;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerController playerController;
    private int idleCounter;
    private int randomNextSmoke;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();   
        idleCounter=0;
        randomNextSmoke = Random.Range(3, 5);
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsWalking",playerController.currentPos != playerController.movePoint.position);
    }

    public void RandomSmokeTrigger()
    {
        idleCounter++;
        if (idleCounter > 2 && randomNextSmoke == idleCounter)
        {
            randomNextSmoke = Random.Range(3, 5);
            idleCounter = 0;
            animator.SetTrigger("Smoke");
        }
    }
    
}
