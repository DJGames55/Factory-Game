using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    private Vector2 moveDirection;

    private bool isJumping;

    private void Start()
    {
        _input.MoveEvent += HandleMove;

        _input.JumpEvent += HandleJump;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void HandleJump()
    {
        isJumping = true;
        JumpTimer();
    }

    private void Move()
    {
        if(moveDirection == Vector2.zero)
        {
            return;
        }

        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * (speed * Time.deltaTime);
    }

    private void Jump()
    {
        if(isJumping)
        {
            transform.position += new Vector3(0, 1, 0) * (jumpSpeed * Time.deltaTime);
        }
    }

    private IEnumerable JumpTimer()
    {
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
        Debug.Log("stop jump")
    }
}