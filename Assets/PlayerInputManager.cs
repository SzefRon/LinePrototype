using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;
        var players = FindObjectsOfType<PlayerController>();
        foreach (var player in players) {
            if (player.index == index) {
                playerController = player;
                break;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerController.input = context.ReadValue<Vector2>().normalized;
    }
}
