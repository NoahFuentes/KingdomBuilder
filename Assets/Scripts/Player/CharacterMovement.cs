using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement Instance;
    private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    private Animator animator;
    [SerializeField] private float deceleration;
    [SerializeField] private float acceleration;
    [SerializeField] private float navAcceleration;
    
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float navRotationSpeed;
    private Vector3 moveDirection;
    private Vector3 inputDirection;
    private float fallMag;
    [SerializeField] private float playerGravity;

    private bool canMove = true;
    [SerializeField] private bool isgrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLM;

    private NavMeshAgent nav;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents unwanted rotations
        nav = GetComponent<NavMeshAgent>();
        nav.speed = PlayerStats.Instance.m_BaseMovementSpeed;
        nav.angularSpeed = navRotationSpeed;
        nav.acceleration = navAcceleration;
        nav.enabled = false;
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!canMove)
        {
            // Gradually slow down
            nav.enabled = false;
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
            //moveDirection = inputDirection.normalized * (PlayerStats.Instance.m_BaseMovementSpeed);

            Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = cameraTransform.right;

            moveDirection = (camForward * moveZ + camRight * moveX).normalized * (PlayerStats.Instance.m_BaseMovementSpeed);

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
        if (nav.enabled && (Vector3.Distance(transform.position, nav.destination) <= nav.stoppingDistance))
        {
            Resource lookat;
            if (( lookat = PlayerInteractions.Instance.resourceInteractable) != null)
                transform.LookAt(lookat.transform.position);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            if (null == PlayerInteractions.Instance.resourceInteractable)
            {
                nav.enabled = false;
                return;
            }

            switch (PlayerInteractions.Instance.resourceInteractable.interactType)
            {
                case ResourceType.CHOPPABLE:

                    break;
                case ResourceType.MINEABLE:

                    break;
                case ResourceType.HARVESTABLE:

                    break;
            }
        }
        if (inputDirection.magnitude > 0)
        {
            if (nav.enabled)
                nav.enabled = false;
            animator.SetBool("isMining", false);
            animator.SetBool("isChopping", false);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(PlayerStats.Instance.m_CurrentStamina > 0)
                {
                    moveDirection *= PlayerStats.Instance.m_SprintSpeedMult;
                    PlayerInteractions.Instance.ReduceStamina(PlayerStats.Instance.m_SprintStaminaCost);
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    NotificationManager.Instance.Notify("Insufficient stamina", Color.red);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isWalking", true);
                }
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
        else if (!nav.enabled)
        {
            // Gradually slow down
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0f, fallMag, 0f), deceleration);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }

    public void navMoveToPosition(Vector3 pos)
    {
        nav.enabled = true;
        nav.SetDestination(pos);
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
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