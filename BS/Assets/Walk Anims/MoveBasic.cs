using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasic : MonoBehaviour
{
    [SerializeField] private float speed = 50f;    // Snelheid van de beweging
    [SerializeField] private float rotSpeed = 50f;  // Rotatiesnelheid
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // Haal de Rigidbody component op
    }

    // Update is called once per frame
    void Update()
    {
        // Bereken de beweging op de Z-as (vooruit/achteruit)
        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = transform.forward * moveVertical;

        // Voeg kracht toe aan de Rigidbody om de speler te bewegen
        rb.MovePosition(transform.position + movement);

        // Draai de speler over de Y-as (links/rechts)
        float turnHorizontal = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnHorizontal, 0));
    }
}
