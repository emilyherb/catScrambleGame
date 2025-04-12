using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{

    private float[] lanePositions = new float[] { -35f, -12f, 12f, 35f };
    private int currentLane = 2;

    public float laneSwitchSpeed = 10f;

    public float jumpForce = 15f;
    public float gravity = -30f;
    private float verticalVelocity = 0f;
    private bool isGrounded = true;

    public float groundHeight = 2f;

    void Start()
    {
        transform.position = new Vector3(lanePositions[currentLane], 6f, transform.position.z);
    }

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
            {
                currentLane--;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < lanePositions.Length - 1)
            {
                currentLane++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 newPosition = transform.position;
        newPosition.y += verticalVelocity * Time.deltaTime;

        if (newPosition.y <= groundHeight)
        {
            newPosition.y = groundHeight;
            verticalVelocity = 0f;
            isGrounded = true;
        }

        UnityEngine.Vector3 targetPos = new UnityEngine.Vector3(lanePositions[currentLane], newPosition.y, transform.position.z);
        newPosition = UnityEngine.Vector3.Lerp(transform.position, targetPos, Time.deltaTime * laneSwitchSpeed * 10f);

        transform.position = newPosition;
    }
}