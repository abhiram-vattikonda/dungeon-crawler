using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomObserver : MonoBehaviour
{
    public static PlayerRoomObserver Instance { get; private set; }

    public event Action<GameObject> OnEnteredANewRoom;
    public event Action<GameObject> OnExitedARoom;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayerEnteredRoom(GameObject roomEntered)
    {
        OnEnteredANewRoom?.Invoke(roomEntered);
    }



    public void PlayerExitedRoom(GameObject roomEntered)
    {
        OnExitedARoom?.Invoke(roomEntered);
    }
}
