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

    public int sensitivity;

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
        // Calculate the target rotation based on the current player rotation and input direction
        float targetRotation = player.transform.rotation.eulerAngles.y + lookDirection.x * (sensitivity * (10 ^ 5)))) * Time.deltaTime;

        // Apply the rotation to the player
        player.transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);

        // Debug information
        Debug.Log($"Target: {targetRotation}");
        Debug.Log($"x: {lookDirection.x}");
        Debug.Log($"y: {lookDirection.y}");
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