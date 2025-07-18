using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance { get; private set; }

    private PlayerInputActions playerInputActions;
    public Vector2 playerMovementValue { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementValue = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }
}
