using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves the player
public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 4f;
    public CharacterController characterController;

    // Gets input and moves player by input
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moving = transform.right * x + transform.forward * z;
        characterController.Move(moving * moveSpeed* Time.deltaTime);

    }
}
