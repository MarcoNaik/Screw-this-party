using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using MovementAndChars;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 startPos;
    public float moveSpeed = 0f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        startPos = player.position;
    }

    private void Update()
    {
        if (startPos != player.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(2,0,0), 
                moveSpeed * Time.fixedDeltaTime);
            
        }
    }

}
