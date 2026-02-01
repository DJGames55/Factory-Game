using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject head;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    public float rotationSpeed = 5f;

    private bool isJumping;

    private void Start()
    {
        _input.MoveEvent += HandleMove;
        _input.JumpEvent += HandleJump;
        _input.LookEvent += HandleLook;

        rb.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Look();
        Jump();
    }

    private void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void HandleLook(Vector2 dir)
    {
        lookDirection = dir;
    }

    private void HandleJump()
    {
        isJumping = true;
    }

    private void Move()
    {
        if(moveDirection == Vector2.zero)
        {
            return;
        }

        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * (speed * Time.deltaTime);
    }

    private void Look()
    {
        if (lookDirection == Vector2.zero)
        {
            return;
        }

        // Calculate rotation angles based on look direction
        float rotationX = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;

        // Create a Quaternion representing the desired rotation around the y-axis
        Quaternion targetRotationY = Quaternion.Euler(0f, rotationX, 0f);

        // Calculate rotation angle on the z-axis based on x-component of look direction
        float rotationZ = lookDirection.x * -10f; // Adjust this value as needed for sensitivity

        // Create a Quaternion representing the desired rotation around the z-axis
        Quaternion targetRotationZ = Quaternion.Euler(0f, 0f, rotationZ);

        // Combine the rotations
        Quaternion targetRotation = targetRotationY * targetRotationZ;

        // Apply the rotation to the player object
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }



    private void Jump()
    {
        if(isJumping)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            isJumping = false;
        }
    }
}