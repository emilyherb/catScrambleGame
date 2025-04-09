using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    // Lane positions
    private float[] lanePositions = new float[] { -35f, -12f, 12f, 35f };
    private int currentLane = 2; // Start in the center-right lane (index 2)

    public float laneSwitchSpeed = 10f; // Lane switch speed

    // Jump variables
    public float jumpForce = 10f; // Increased for higher jump
    public float gravity = -9.81f; // Stronger gravity for quicker jump
    private float verticalVelocity = 0f; // Tracks the player's Y speed
    private bool isGrounded = true; // Checks if the player is on the ground

    // Ground level adjustment
    public float groundHeight = 2f; // Capsule center at Y = 2, bottom at Y = 1 (road level)

    void Start()
    {
        // Set initial position above the ground (Y = 6 for Capsule center)
        transform.position = new Vector3(lanePositions[currentLane], 6f, transform.position.z);
    }

    void Update()
    {
        // Lane switching input
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < lanePositions.Length - 1)
        {
            currentLane++;
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpForce; // Apply upward force
            isGrounded = false; // Player is now in the air
        }

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Update Y position based on vertical velocity
        Vector3 newPosition = transform.position;
        newPosition.y += verticalVelocity * Time.deltaTime;

        // Check if player hits the ground (adjusted for road height)
        if (newPosition.y <= groundHeight)
        {
            newPosition.y = groundHeight; // Snap to road level
            verticalVelocity = 0f; // Reset velocity
            isGrounded = true; // Player is grounded again
        }

        // Smooth lane switching on X-axis
        UnityEngine.Vector3 targetPos = new UnityEngine.Vector3(lanePositions[currentLane], newPosition.y, transform.position.z);
        newPosition = UnityEngine.Vector3.Lerp(transform.position, targetPos, Time.deltaTime * laneSwitchSpeed * 10f);

        // Apply the new position
        transform.position = newPosition;
    }
}