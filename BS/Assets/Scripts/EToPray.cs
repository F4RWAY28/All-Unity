using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EToPray : MonoBehaviour
{
    public Animator animator; // Drag your Animator component here in the Inspector
    public string pray; // The name of the animation to play

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.Play(pray);
        }
    }
}
