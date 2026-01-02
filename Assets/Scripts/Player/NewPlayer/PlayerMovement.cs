using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Movement")]
    public float gravity = -20f;

    [Header("Slope")]
    [Range(0f, 89f)] public float maxSlopeAngle = 50f;
    [Range(0f, 89f)] public float slowdownStartAngle = 35f;
    public float groundCheckDistance = 1.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 groundNormal = Vector3.up;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdateGroundNormal();
        HandleMovement();
        ApplyGravity();
    }

    void UpdateGroundNormal()
    {
        if (!controller.isGrounded)
        {
            groundNormal = Vector3.up;
            return;
        }

        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.2f;

        if (Physics.Raycast(origin, Vector3.down, out hit, groundCheckDistance))
            groundNormal = hit.normal;
        else
            groundNormal = Vector3.up;
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // ?? Authoritative input direction (XZ only)
        Vector3 inputDir = camForward * v + camRight * h;

        if (inputDir.sqrMagnitude < 0.0001f)
            return;

        inputDir.Normalize();

        // --- Directional slope calculation ---
        // Sample slope in the direction of movement
        Vector3 testDir = inputDir;
        testDir.y = -(groundNormal.x * testDir.x + groundNormal.z * testDir.z) / groundNormal.y;

        float directionalSlopeAngle = Vector3.Angle(
            new Vector3(testDir.x, 0f, testDir.z),
            testDir
        );

        bool isUphill = testDir.y > 0f;

        float slopeMultiplier = 1f;

        if (isUphill && directionalSlopeAngle > slowdownStartAngle)
        {
            slopeMultiplier = Mathf.InverseLerp(
                maxSlopeAngle,
                slowdownStartAngle,
                directionalSlopeAngle
            );

            if (directionalSlopeAngle > maxSlopeAngle)
                return; // blocked ONLY in this direction
        }

        // --- Final movement: SAME direction, Y solved ---
        Vector3 finalMove = inputDir;

        finalMove.y = -(groundNormal.x * finalMove.x + groundNormal.z * finalMove.z) / groundNormal.y;

        controller.Move(finalMove * PlayerStats.Instance.m_BaseMovementSpeed * slopeMultiplier * Time.deltaTime);
    }





    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
