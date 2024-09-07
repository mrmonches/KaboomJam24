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
    [SerializeField] private Vector2 JumpForce;

    private float moveValue;

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

    private void OnJump()
    {
        if (canJump)
        {
            _rb2d.AddForce(JumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(moveValue, _rb2d.velocity.y);
    }

    private void OnDestroy()
    {
        moveAction.started -= MoveAction_started;
        moveAction.canceled -= MoveAction_canceled;
    }
}
