using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private PlayerStats ps;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private float deceleration;
    [SerializeField] private float acceleration;
    
    [SerializeField] private float rotationSpeed;
    private Vector3 moveDirection;
    private Vector3 inputDirection;
    private float fallMag;
    [SerializeField] private float playerGravity;

    private bool canMove = true;
    [SerializeField] private bool isgrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLM;

    void Start()
    {
        ps = GetComponent<PlayerStats>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotations

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            // Gradually slow down
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0f, fallMag, 0f), deceleration);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            return;
        }
        // Get input from the player
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(moveX, 0f, moveZ);

        if (inputDirection.magnitude > 0)
        {
            // Normalize to prevent faster diagonal movement and apply speed
            moveDirection = inputDirection.normalized * (ps.m_BaseMovementSpeed);
        }
        else
        {
            // No input, so stop applying movement force
            moveDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLM);
        if (!isgrounded)
        {
            fallMag += playerGravity * Time.fixedDeltaTime;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.up * fallMag, -playerGravity * Time.fixedDeltaTime);
        }
        else
        {
            fallMag = 0;
        }

        if (!canMove) return;
        if (inputDirection.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && ps.m_CurrentStamina > 0)
            {
                moveDirection *= ps.m_SprintSpeedMult;
                PlayerInteractions.Instance.ReduceStamina(ps.m_SprintStaminaCost);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
            // face correct direction
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);

            // Apply movement smoothly
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(moveDirection.x, fallMag, moveDirection.z), acceleration);

        }
        else
        {
            // Gradually slow down
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0f, fallMag, 0f), deceleration);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }
    public void EnableMovement()
    {
        canMove = true;
    }
}