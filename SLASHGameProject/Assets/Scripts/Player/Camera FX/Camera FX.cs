using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    // Reference to the camera's field of view
    private Camera camera;

    // Default and running FOV values
    public float defaultFOV = 60f;
    public float runningFOV = 80f;

    // Sensitivity factor for smooth transition (optional)
    public float fovSpeed = 5f;

    void Start()
    {
        // Get the Camera component attached to the object
        camera = Camera.main;
        // Set the initial FOV to default FOV
        camera.fieldOfView = defaultFOV;
    }

    void Update()
    {
        // Check if both W and SHIFT are pressed for running
        bool isRunning = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift);

        // Set the camera FOV based on running state
        float targetFOV = isRunning ? runningFOV : defaultFOV;

        // Smoothly transition to the target FOV
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * fovSpeed);
    }
}
