using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject head;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    public float sensitivity;

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

        sensitivity = _gameManager.GetComponent<GameManager>().sens;
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
        Jump();
    }

    private void Move()
    {
        if (moveDirection == Vector2.zero)
        {
            return;
        }

        // Get the forward vector of the player's transform
        Vector3 forward = player.transform.forward;

        // Calculate the movement direction based on the forward vector and input direction
        Vector3 movement = forward * moveDirection.y + player.transform.right * moveDirection.x;

        // Normalize the movement vector to ensure consistent movement speed diagonally
        movement.Normalize();

        // Apply the movement to the player's position
        transform.position += movement * (speed * Time.deltaTime);
    }


    private void Look()
    {
        // Calculate the target rotation based on the current player rotation and input direction
        float targetRotation = player.transform.rotation.eulerAngles.y + lookDirection.x * sensitivity * Time.deltaTime;

        // Apply the rotation to the player
        player.transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);

        // Ensure target rotation is between 0 and 360
        targetRotation = Mathf.Repeat(targetRotation, 360f);
    }

    private bool isGrounded;

    // Collision
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object is tagged as ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        // Reset isGrounded when no longer in contact with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }
}