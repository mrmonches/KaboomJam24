using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction interactAction;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float SprintMultiplier;
    [SerializeField] private float MaxFallSpeed;

    [SerializeField] private float GravityMultiplier;
    [SerializeField] private float defaultGravity = 1f;

    [SerializeField] private float InvinsibilityTimer;
    [SerializeField] private float invinsibilityCounter;

    private float moveValue;

    // Variables that are redundant but help clarity in conditional statements
    private bool isJumping;
    private bool isSprinting;
    private bool isMidair;
    private bool isInteracting;
    private static bool isMixing;

    private bool canUI;

    [SerializeField, Tooltip("The buffer between when the player loses contact with the ground and they are still able to jump")]
    private float CoyoteTimer;
    private float coyoteTimeCounter;

    [SerializeField, Tooltip("The buffer between when the player hits jump before hitting the ground")]
    private float BufferTimer;
    private float bufferTimeCounter;

    [SerializeField, Tooltip("The amount of time the player is allowed to hold jump and receive force")] 
    private float JumpTimer;
    private float currentJumpTime;

    [SerializeField] private Vector2 BoxCastSize;
    [SerializeField] private float GroundDistance;
    [SerializeField] private LayerMask GroundMask;

    [SerializeField] private GameObject DrinkMenu;

    [SerializeField] private PlayerInteractiveController InteractionController;

    private Rigidbody2D _rb2d;

    private PlayerInventoryController playerInventory;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip JumpClip;

    private SpriteRenderer _spriteRenderer;

    private bool decreaseOpacity;
    [SerializeField] private float opacityChange;
    [SerializeField] private float opacityTimer;
    private float opacityCounter;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        moveAction = _playerInput.currentActionMap.FindAction("Move");
        jumpAction = _playerInput.currentActionMap.FindAction("Jump");
        sprintAction = _playerInput.currentActionMap.FindAction("Sprint");
        interactAction = _playerInput.currentActionMap.FindAction("Interact");

        moveAction.started += MoveAction_started;
        moveAction.canceled += MoveAction_canceled;

        jumpAction.started += JumpAction_started;
        jumpAction.canceled += JumpAction_canceled;

        sprintAction.started += SprintAction_started;
        sprintAction.canceled += SprintAction_canceled;

        interactAction.started += InteractAction_started;
        interactAction.canceled += InteractAction_canceled;

        _rb2d = GetComponent<Rigidbody2D>();

        playerInventory = GetComponent<PlayerInventoryController>();

        currentJumpTime = JumpTimer;

        _audioSource = GetComponent<AudioSource>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void MoveAction_started(InputAction.CallbackContext obj)
    {
        moveValue = moveAction.ReadValue<float>() * MoveSpeed;

        if (moveAction.ReadValue<float>() > 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
        else if (moveAction.ReadValue<float>() < 0 && _spriteRenderer.flipX) 
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        moveValue = 0f;
    }

    private void JumpAction_started(InputAction.CallbackContext obj)
    {
        bufferTimeCounter = BufferTimer;

        if (coyoteTimeCounter > 0 && !isJumping && bufferTimeCounter > 0)
        {
            isJumping = true;

            coyoteTimeCounter = 0;

            //_audioSource.PlayOneShot(JumpClip);
        }
    }

    private void JumpAction_canceled(InputAction.CallbackContext obj)
    {
        if (isJumping)
        {
            isJumping = false;

            currentJumpTime = JumpTimer;

            coyoteTimeCounter = 0;
        }
    }

    private void SprintAction_started(InputAction.CallbackContext obj)
    {
        isSprinting = true;
    }
    private void SprintAction_canceled(InputAction.CallbackContext obj)
    {
        isSprinting = false;
    }   
    
    private void InteractAction_started(InputAction.CallbackContext obj)
    {
        if (invinsibilityCounter <= 0)
        {
            isInteracting = true;

            if (canUI)
            {
            //    InteractionController.ProgressCircle();
            }
        }
    }

    private void InteractAction_canceled(InputAction.CallbackContext obj)
    {
        if (invinsibilityCounter <= 0)
        {
            isInteracting = false;
        }
    }

    private void PlayerJump()
    {
        if (currentJumpTime >= 0)
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

    public void ActivateIFrames()
    {
        if (invinsibilityCounter <= 0)
        {
            invinsibilityCounter = InvinsibilityTimer;

            decreaseOpacity = true;

            opacityCounter = opacityTimer;

            playerInventory.LoseItems();
        }
    }

    private void IFrameOpacityEffect()
    {
        if (decreaseOpacity)
        {
            _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a - opacityChange);
        }

        if (!decreaseOpacity)
        {
            _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a + opacityChange);
        }
    }

    public bool GetInteractionStatus()
    {
        return isInteracting;
    }

    private void GroundCheck()
    {
        RaycastHit2D BoxCasthit = BoxCastDrawer.BoxCastAndDraw(transform.position, BoxCastSize, 0, Vector2.down, GroundDistance, GroundMask);

        if (BoxCasthit.transform != null)
        {
            coyoteTimeCounter = CoyoteTimer;

            if (bufferTimeCounter > 0)
            {
                //_audioSource.PlayOneShot(JumpClip);

                PlayerJump();
            }

            isMidair = false;
        }
        else
        {
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            isMidair = true;
        }
    }

    public void InteractiveUIStatus(bool status)
    {
        canUI = status;
    }

    public bool CheckUIStatus()
    {
        return canUI;
    }

    public void ManageDrinkMenuStatus(CustomerController customer)
    {
        if (!isMixing && customer != null)
        {
            DrinkMenu.SetActive(true);
            DrinkMenu.GetComponentInChildren<DrinkMenuManager>().NewOrder(customer);
            isMixing = true;
        }
        else
        {
            isMixing = false;
            DrinkMenu.SetActive(false);
        }
    }

    public static void SetMixingStatus(bool status)
    {
        isMixing = status;
        
    }

    private void FixedUpdate()
    {
        if (!isMixing) 
        {
            if (isSprinting)
            {
                _rb2d.velocity = new Vector2(moveValue * SprintMultiplier, Mathf.Max(_rb2d.velocity.y, -MaxFallSpeed));
            }
            else
            {
                _rb2d.velocity = new Vector2(moveValue, Mathf.Max(_rb2d.velocity.y, -MaxFallSpeed));
            }

            if (isJumping)
            {
                PlayerJump();
            }

            GroundCheck();

            if (isMidair && _rb2d.velocity.y < 0)
            {
                _rb2d.gravityScale *= GravityMultiplier;
            }
            else
            {
                _rb2d.gravityScale = defaultGravity;
            }

            if (bufferTimeCounter > 0)
            {
                bufferTimeCounter -= Time.deltaTime;
            }

            if (invinsibilityCounter > 0)
            {
                invinsibilityCounter -= Time.deltaTime;

                IFrameOpacityEffect();

                opacityCounter -= Time.deltaTime;

                if (opacityCounter < 0)
                {
                    opacityCounter = opacityTimer;
                    decreaseOpacity = !decreaseOpacity;
                }
            }
            else
            {
                _spriteRenderer.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            _rb2d.velocity = Vector2.zero;
        }
    }

    private void OnDestroy()
    {
        moveAction.started -= MoveAction_started;
        moveAction.canceled -= MoveAction_canceled;

        jumpAction.started -= JumpAction_started;
        jumpAction.canceled -= JumpAction_canceled;

        sprintAction.started -= SprintAction_started;
        sprintAction.canceled -= SprintAction_canceled;

        interactAction.started -= InteractAction_started;
        interactAction.canceled -= InteractAction_canceled;
    }
}