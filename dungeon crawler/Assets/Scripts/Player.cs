using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance {  get; private set; }
    private float playerSpeed = 10f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveVector = PlayerInputManager.instance.playerMovementValue;
        transform.position += new Vector3(moveVector.x, moveVector.y, 0) * Time.deltaTime * playerSpeed;
    }
}
