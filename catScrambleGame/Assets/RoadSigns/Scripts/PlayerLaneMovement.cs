using UnityEngine;
using System.Collections;

public class PlayerLaneMovement : MonoBehaviour
{
    private float[] lanePositions = new float[] { -35f, -12f, 12f, 35f };
    private int currentLane = 2;
    public float laneSwitchSpeed = 10f;
    public float jumpForce = 100f;
    public float gravity = -200f;
    private float verticalVelocity = 0f;
    private bool isGrounded = true;
    public float groundHeight = 2f;
    private Renderer catRenderer;
    private Color originalColor;
    public float flashDuration = 0.2f;
    public AudioClip hitSound;
    private AudioSource audioSource;
    private CameraShake cameraShake;

    void Start()
    {
        transform.position = new Vector3(lanePositions[currentLane], 6f, transform.position.z);

        catRenderer = GetComponentInChildren<Renderer>();
        if (catRenderer != null)
        {
            originalColor = catRenderer.material.color;
        }
        else
        {
            Debug.LogWarning("No Renderer found on Cat (1) or its children!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on Cat (1)!");
        }

        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogWarning("No CameraShake component found on Main Camera!");
        }
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

        Vector3 targetPos = new Vector3(lanePositions[currentLane], newPosition.y, transform.position.z);
        newPosition = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * laneSwitchSpeed * 10f);
        transform.position = newPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Cat hit an obstacle!");
            StartCoroutine(FlashCat());
            PlayHitSound();
            if (cameraShake != null)
            {
                cameraShake.Shake();
            }
        }
    }

    private IEnumerator FlashCat()
    {
        if (catRenderer == null) yield break;

        catRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        catRenderer.material.color = originalColor;
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}