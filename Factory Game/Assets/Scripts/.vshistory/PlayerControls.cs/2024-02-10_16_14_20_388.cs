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

    public float rotationSpeed;

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

        // Calculate the target rotation angle
        float rotationAngle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;

        // Smoothly rotate towards the target rotation
        Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        player.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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