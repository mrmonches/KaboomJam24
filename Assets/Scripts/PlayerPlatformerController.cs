using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction moveAction;
    private InputAction jumpAction;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpForce;

    private float moveValue;

    private bool isJumping;
    private float jumpValue;

    [SerializeField] private float JumpTimer;
    [SerializeField] private float currentJumpTime;

    private bool canJump;

    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        moveAction = _playerInput.currentActionMap.FindAction("Move");
        jumpAction = _playerInput.currentActionMap.FindAction("Jump");

        moveAction.started += MoveAction_started;
        moveAction.canceled += MoveAction_canceled;

        jumpAction.started += JumpAction_started;
        jumpAction.canceled += JumpAction_canceled;

        _rb2d = GetComponent<Rigidbody2D>();

        canJump = true;
    }

    private void MoveAction_started(InputAction.CallbackContext obj)
    {
        moveValue = moveAction.ReadValue<float>() * MoveSpeed;
    }

    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        moveValue = 0f;
    }

    private void JumpAction_started(InputAction.CallbackContext obj)
    {
        if (canJump && !isJumping)
        {
            isJumping = true;
        }
    }

    private void JumpAction_canceled(InputAction.CallbackContext obj)
    {
        if (isJumping)
        {
            isJumping = false;

            currentJumpTime = JumpTimer;
        }
    }

    private void PlayerJump()
    {
        if (currentJumpTime > 0)
        {
            currentJumpTime -= Time.deltaTime;

            _rb2d.velocity = new Vector2(_rb2d.velocity.x, JumpForce);
        }
        else
        {
            currentJumpTime = JumpTimer;

            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(moveValue, _rb2d.velocity.y);

        if (isJumping)
        {
            PlayerJump();
        }
    }

    private void OnDestroy()
    {
        moveAction.started -= MoveAction_started;
        moveAction.canceled -= MoveAction_canceled;

        jumpAction.started -= JumpAction_started;
        jumpAction.canceled -= JumpAction_canceled;
    }
}
