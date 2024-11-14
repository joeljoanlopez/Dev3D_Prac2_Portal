using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("Movement Settings")] public float acceleration = 5f;

    public float walkMaxSpeed = 7f;
    public float runMaxSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    [Header("Camera Settings")]
    public Transform pitchController;
    public float cameraSensitivity = 1f;
    public float maxPitch = 90f;
    public float minPitch = -90f;

    private CharacterController characterController;
    private CollisionFlags collisionFlags;
    private float horizontalRotation;
    private bool isGrounded;
    private Vector2 lookInput;
    private Vector2 moveInput;
    private PlayerInput playerInput;
    private Vector3 velocity = Vector3.zero;
    private float verticalRotation;
    private Vector3 movementDirection;
    public Vector3 MovementDirection { get { return movementDirection; } }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        horizontalRotation = transform.eulerAngles.y;
        verticalRotation = pitchController.transform.eulerAngles.x;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCamera();
        collisionFlags = characterController.Move(velocity * Time.deltaTime);
        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            velocity.y = 0f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    /// Reads the input for movement and stores it in moveInput
    public void OnMove()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public void OnLook()
    {
        lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        movementDirection = (moveInput.x * transform.right + moveInput.y * transform.forward).normalized;
        float maxSpeed = playerInput.actions["Run"].IsPressed() ? runMaxSpeed : walkMaxSpeed;
        Vector3 targetVelocity = movementDirection * maxSpeed;
        velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded)
            velocity.y = jumpForce;
        else
            velocity.y -= gravity * Time.deltaTime;
    }

    private void HandleCamera()
    {
        horizontalRotation = lookInput.x * cameraSensitivity * Time.deltaTime;

        verticalRotation -= lookInput.y * cameraSensitivity * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minPitch, maxPitch);

        transform.Rotate(Vector3.up * horizontalRotation);
        pitchController.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}