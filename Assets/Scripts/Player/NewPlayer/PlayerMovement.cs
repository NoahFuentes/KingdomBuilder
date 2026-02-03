using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private PlayerStats stats;
    private CharacterController controller;

    private Vector3 moveInput;

     public bool canMove;
     public bool canJump;
     public bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float gravity;

     public float currentMovementSpeed;

    private Vector3 GetGroundNormal()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.25f))
            return hit.normal;
        return Vector3.up;
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        canMove = true;

        stats = PlayerStats.Instance;
        currentMovementSpeed = stats.m_BaseMovementSpeed;

        controller = GetComponent<CharacterController>();
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

        //gather move input and apply speed to normalized move vector
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if(moveInput.magnitude > 1f) moveInput.Normalize();
        moveInput *= currentMovementSpeed;
        
    }
    private void FixedUpdate() //handle movement
    {
        //apply gravity
        if(!isGrounded)
            controller.Move(new Vector3(0f, -gravity, 0f));

        Vector3 groundNormal = GetGroundNormal();
        Vector3 slopeMove = Vector3.ProjectOnPlane(moveInput, groundNormal);
        controller.Move(slopeMove);
    }
    








    //MAKE IT DIRECTIONAL TO THE CAMERA
    //ADD GRAVITY
    //TURN THE CHARACTER

    //ADD JUMPING

}
