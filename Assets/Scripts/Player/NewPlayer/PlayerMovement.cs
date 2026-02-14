using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private PlayerStats stats;
    private CharacterController controller;

    private Vector3 moveInput;
    private Vector3 velocity;
    private Transform cam;
    private Animator animator;


    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canJump;
    [HideInInspector] public bool isGrounded;

    [Header("Player Grounding")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float airDrag;

    [Header("Movement")]
     public float currentMovementSpeed;
    [SerializeField] private float turnSpeed;
    private float currentRotationalVel;
    [SerializeField] private float maxSlope;
    [SerializeField] private float jumpHeight;
    private bool jumpRequested = false;

    private Vector3 GetGroundNormal()
    {
        if(Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, 0.5f, groundLayerMask))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            //Debug.Log(angle);
            if (angle <= maxSlope)
                return hit.normal;
        }
        return Vector3.up;
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        stats = PlayerStats.Instance;
        currentMovementSpeed = stats.m_BaseMovementSpeed;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        cam = Camera.main.transform;
    }
    private void Update() //get inputs
    {
        if (!canMove) return;


        //Get correct movespeed
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.25f, groundLayerMask);
        if (!isGrounded)
            currentMovementSpeed = stats.m_InAirSpeed;
        else if (Input.GetKey(KeyCode.LeftShift))
            currentMovementSpeed = stats.m_BaseMovementSpeed * stats.m_SprintSpeedMult;
        else
            currentMovementSpeed = stats.m_BaseMovementSpeed;

        //gather move input and apply speed to normalized move vector based on camera rotation
        Vector3 camForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        moveInput = (camRight * Input.GetAxis("Horizontal")) + (camForward * Input.GetAxis("Vertical"));
        if(moveInput.magnitude > 1f) moveInput.Normalize();
        
        if (canJump && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }
    private void FixedUpdate() //handle movement
    {
        if(jumpRequested && isGrounded)
        {
            jumpRequested = false;
            velocity.y = jumpHeight;
            animator.SetTrigger("Jump");
        }


        //Play inAir animation
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            // Build velocity from input only while grounded
            Vector3 groundNormal = GetGroundNormal();
            Vector3 wishMove = Vector3.ProjectOnPlane(moveInput, groundNormal);

            velocity.x = wishMove.x * currentMovementSpeed;
            velocity.z = wishMove.z * currentMovementSpeed;
            animator.SetFloat("Speed", (new Vector3(velocity.x, 0f, velocity.z).magnitude) / (stats.m_BaseMovementSpeed * stats.m_SprintSpeedMult));
        }
        else
        {
            

            //small air control
            Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
            horizontal *= Mathf.Clamp01(1f - airDrag * Time.fixedDeltaTime);

            Vector3 controlForce = moveInput * stats.m_InAirSpeed * Time.fixedDeltaTime;
            horizontal += controlForce;

            velocity.x = horizontal.x;
            velocity.z = horizontal.z;
            //velocity.x = Mathf.Lerp(velocity.x, airWish.x, airControl);
            //velocity.z = Mathf.Lerp(velocity.z, airWish.z, airControl);

            // Gravity
            //velocity.y = Mathf.Lerp(velocity.y, -maxFallSpeed, gravity);
            velocity.y -= gravity * Time.fixedDeltaTime;
            velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);
        }

        Vector3 flatVel = new Vector3(velocity.x, 0, velocity.z);
        if (flatVel.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(flatVel.x, flatVel.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref currentRotationalVel,
                1f / turnSpeed
            );
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        controller.Move(velocity * Time.fixedDeltaTime);
    }
    


}
