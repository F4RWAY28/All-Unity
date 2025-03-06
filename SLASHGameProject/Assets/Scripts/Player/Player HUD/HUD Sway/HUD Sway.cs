using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSway : MonoBehaviour
{
    public RectTransform hudElement;   // The HUD element to sway
    public Camera playerCamera;        // The player's camera
    public float swayAmount = 10f;     // How much the HUD sways based on movement
    public float rotationSwayAmount = 15f; // How much the HUD sways based on rotation
    public float swaySpeed = 5f;       // How fast the HUD reacts to movement
    public float smoothing = 5f;       // How smoothly the HUD catches up
    public float maxSwayDistance = 20f; // Max distance the HUD can sway from its initial position

    private Vector3 initialPosition;   // Initial position of the HUD element
    private Vector3 lastCameraPosition; // Stores the camera’s last position
    private Vector2 lastMousePosition; // Stores the last mouse position
    private Vector3 velocity;           // Stores calculated velocity
    private Vector2 mouseDelta;         // Stores mouse movement delta

    void Start()
    {
        // Store initial HUD position
        initialPosition = hudElement.localPosition;
        lastCameraPosition = playerCamera.transform.position;
        lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    void Update()
    {
        // Calculate camera movement velocity
        velocity = (playerCamera.transform.position - lastCameraPosition) / Time.deltaTime;
        lastCameraPosition = playerCamera.transform.position;

        // Calculate mouse movement delta
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouseDelta = currentMousePosition - lastMousePosition;
        lastMousePosition = currentMousePosition;

        // Invert velocity for natural HUD sway effect
        Vector3 movementSway = -velocity * swayAmount * 0.1f;

        // Fixed rotation sway for correct vertical movement
        Vector3 rotationSway = new Vector3(-mouseDelta.x, -mouseDelta.y, 0f) * rotationSwayAmount * 0.015f;

        // Target position based on both movement and rotation sway
        Vector3 targetPosition = initialPosition + movementSway + rotationSway;

        // **Clamp target position within maxSwayDistance**
        targetPosition.x = Mathf.Clamp(targetPosition.x, initialPosition.x - maxSwayDistance, initialPosition.x + maxSwayDistance);
        targetPosition.y = Mathf.Clamp(targetPosition.y, initialPosition.y - maxSwayDistance, initialPosition.y + maxSwayDistance);

        // Smoothly move the HUD towards the clamped target position
        hudElement.localPosition = Vector3.Lerp(hudElement.localPosition, targetPosition, Time.deltaTime * smoothing);
    }
}
