using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public Rigidbody Player;
    public float wallRunGravity = 0.98f; // How slow the player falls while wall running
    public float maxWallRunTime = 2f;   // Maximum time player can wall run
    public float wallCheckDistance = 0.8f; // Distance for detecting walls

    // Wall Jumping Variables
    public float wallJumpForce = 0.28f;    // Increased upward force for a higher wall jump
    public float wallJumpBoost = 0.17f;    // Forward boost after wall jump
    public float wallJumpHeightMultiplier = 0.002f; // Multiplier to make the jump higher

    private bool isWallRunning = false;
    private float wallRunTimer = 0f;
    private Vector3 lastWallNormal; // Stores the wall normal to determine direction
    private float storedSpeed; // Store speed before wall run

    // Reference to the Movement script to check wall running state
    private Movement movementScript;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
        movementScript = GetComponent<Movement>(); // Get the Movement script component
    }

    void Update()
    {
        bool isInAir = !Physics.Raycast(transform.position, Vector3.down, 1.1f); // Check if in air
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool holdingWallRunKey = Input.GetKey(KeyCode.LeftShift); // Left Shift required
        bool pressingJump = Input.GetKey(KeyCode.Space); // Space required

        // Check for walls on left and right
        bool wallLeft = Physics.Raycast(transform.position, -transform.right, out RaycastHit leftWallHit, wallCheckDistance);
        bool wallRight = Physics.Raycast(transform.position, transform.right, out RaycastHit rightWallHit, wallCheckDistance);

        if (isInAir && holdingWallRunKey && isMoving && (wallLeft || wallRight))
        {
            if (!isWallRunning)
            {
                StartWallRun(wallLeft ? leftWallHit.normal : rightWallHit.normal);
            }
        }
        else
        {
            StopWallRun();
        }

        if (isWallRunning)
        {
            wallRunTimer += Time.deltaTime;
            if (wallRunTimer >= maxWallRunTime)
            {
                StopWallRun();
            }

            // Wall Jumping Mechanic (requires Shift + Space + a movement key)
            if (pressingJump && holdingWallRunKey && isMoving)
            {
                PerformWallJump();
            }
        }
    }

    void StartWallRun(Vector3 wallNormal)
    {
        isWallRunning = true;
        lastWallNormal = wallNormal;
        wallRunTimer = 0f;

        // Store current speed before wall run
        storedSpeed = Player.velocity.magnitude;

        // Apply forward momentum based on last movement speed
        Vector3 wallRunDirection = Vector3.Cross(lastWallNormal, Vector3.up).normalized * storedSpeed;
        if (Vector3.Dot(wallRunDirection, transform.forward) < 0) // Ensure correct direction
        {
            wallRunDirection = -wallRunDirection;
        }

        Player.velocity = wallRunDirection;
        Player.useGravity = false;
    }

    void StopWallRun()
    {
        if (isWallRunning)
        {
            isWallRunning = false;
            Player.useGravity = true;
        }
    }

    void FixedUpdate()
    {
        if (isWallRunning)
        {
            // Apply slow descent while wall running
            Player.velocity = new Vector3(Player.velocity.x, -wallRunGravity, Player.velocity.z);
        }
    }

    void PerformWallJump()
    {
        if (isWallRunning)
        {
            StopWallRun();

            // Get the player's current movement direction (ignore y-axis for horizontal direction)
            Vector3 movementDirection = new Vector3(Player.velocity.x, 0, Player.velocity.z).normalized;

            // Check if the player is moving forward or backward by comparing the movement direction to the wall's normal
            float movementDot = Vector3.Dot(movementDirection, transform.forward);

            // If the player is moving backward, reverse the wall jump boost direction
            Vector3 jumpDirection = lastWallNormal * 1.5f + (movementDot < 0 ? -movementDirection : movementDirection) * wallJumpBoost;
            jumpDirection.y = wallJumpForce * wallJumpHeightMultiplier; // Increase jump height

            // Preserve horizontal velocity from wall run
            Vector3 currentVelocity = Player.velocity;
            currentVelocity.y = 0; // Reset vertical velocity

            // Apply jump velocity while keeping the horizontal momentum in the same direction
            Player.velocity = jumpDirection + currentVelocity;
        }
    }

    // Method to detect if the player is wall running from the Movement script
    public bool IsWallRunning()
    {
        return isWallRunning;
    }
}