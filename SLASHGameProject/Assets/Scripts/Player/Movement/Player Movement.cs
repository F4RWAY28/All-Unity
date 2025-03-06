using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody Player;

    // Walking and running speeds
    public float maxWalkingSpeed = 2.5f;
    public float maxRunningSpeed = 25f;

    // Acceleration and Deceleration
    public float acceleration = 18f;
    public float deceleration = 8f;

    // Jumping settings
    public float minJumpForce = 2f;
    public float maxJumpForce = 4f;
    public float maxHoldTime = 1f;
    private bool isGrounded = false;
    private float jumpHoldTime = 0f;
    private bool chargingJump = false;

    // Reference to the WallRunning script
    private WallRunning wallRunningScript;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Player.freezeRotation = true;
        wallRunningScript = GetComponent<WallRunning>();
    }

    void Update()
    {
        // Ground check using Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f);

        // Detect movement state
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        float speedCap = isRunning ? maxRunningSpeed : maxWalkingSpeed;

        // Movement input
        Vector3 targetVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) targetVelocity += transform.forward;
        if (Input.GetKey(KeyCode.S)) targetVelocity -= transform.forward;
        if (Input.GetKey(KeyCode.A)) targetVelocity -= transform.right;
        if (Input.GetKey(KeyCode.D)) targetVelocity += transform.right;

        if (targetVelocity.magnitude > 0)
        {
            targetVelocity = targetVelocity.normalized * speedCap;

            // Wall slide check
            if (Physics.Raycast(transform.position, targetVelocity.normalized, out RaycastHit wallHit, 0.6f) && !wallRunningScript.IsWallRunning())
            {
                targetVelocity = Vector3.ProjectOnPlane(targetVelocity, wallHit.normal);
            }

            // Apply movement with acceleration
            Player.velocity = Vector3.Lerp(Player.velocity, new Vector3(targetVelocity.x, Player.velocity.y, targetVelocity.z), Time.deltaTime * acceleration);
        }
        else
        {
            // Deceleration
            Vector3 horizontalVelocity = new Vector3(Player.velocity.x, 0, Player.velocity.z);
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * deceleration);
            Player.velocity = new Vector3(horizontalVelocity.x, Player.velocity.y, horizontalVelocity.z);
        }

        // Jump charging
        if (isGrounded && !wallRunningScript.IsWallRunning())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                chargingJump = true;
                jumpHoldTime = 0f;
            }

            if (chargingJump && Input.GetKey(KeyCode.Space))
            {
                jumpHoldTime += Time.deltaTime;
                jumpHoldTime = Mathf.Clamp(jumpHoldTime, 0f, maxHoldTime);
            }

            if (chargingJump && Input.GetKeyUp(KeyCode.Space))
            {
                float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, jumpHoldTime / maxHoldTime);
                Player.velocity = new Vector3(Player.velocity.x, jumpForce, Player.velocity.z);
                chargingJump = false;
            }
        }
    }
}

