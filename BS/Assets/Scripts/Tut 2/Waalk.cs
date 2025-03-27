using UnityEngine;

public class Waalk : MonoBehaviour
{
    public Rigidbody rb; // Assign the Rigidbody of the character
    public Animator animator; // Assign the Animator component
    public float moveForce = 10f; // Adjust movement speed

    private bool isWalking = false;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isWalking)
            {
                animator.SetTrigger("Walking"); // Play the Walk animation
                isWalking = true;
            }

            // Apply force to move the character forward
            rb.AddForce(transform.forward * moveForce, ForceMode.Force);
        }
        else if (isWalking)
        {
            animator.SetTrigger("Idle"); // Play the Idle animation
            isWalking = false;
        }
    }
}
