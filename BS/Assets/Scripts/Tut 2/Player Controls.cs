using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerJump : MonoBehaviour
{
    public float forceAmount = 12f;  // Jump force
    public float moveforceAmount = 2f;
    public float rotationAmount = 1f;
    private Rigidbody rb;
    private bool isGrounded;  // Tracks whether the player is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get Rigidbody component
    }

    void Update()
    {
        // Check if SPACE is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
            isGrounded = false;  // Prevent jumping again mid-air
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveforceAmount, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -moveforceAmount, ForceMode.Force);
        }

        float x = Input.GetAxis("Horizontal");

        transform.Rotate(0f, x * 5f, 0f);
        
    }

    // Detect when the player touches the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // Make sure your ground objects have the "Ground" tag
        {
            isGrounded = true;
        }
    }
}
