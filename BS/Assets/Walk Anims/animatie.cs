using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class animatie : MonoBehaviour

{
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        //Pak het animator component en sla die op in de variabele
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        //Check voor verticale input
        if (Input.GetAxis("Vertical") > 0)
        {
            //is de waarde groter dan 0 dan heb je een knop naar boven ingedrukt
            //Roep de juiste trigger aan!
            ani.SetTrigger("Walk 0");
            //SetTrigger is trigger activeren
            ani.ResetTrigger("Idle 0");
            ani.ResetTrigger("WalkR");
            //ResetTrigger is Trigger de-activeren
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            //is de waarde kleiner dan 0 dan heb je een knop naar beneden ingedrukt
            //Roep de juiste trigger aan
            ani.SetTrigger("WalkR");
            ani.ResetTrigger("Idle 0");
            ani.ResetTrigger("Walk 0");
        }
        else
        {
            //is de waarde 0 dan heb je niets ingedrukt
            //Roep de juiste trigger aan
            ani.SetTrigger("Idle 0");
            ani.ResetTrigger("Walk 0");
            ani.ResetTrigger("WalkR");
        }
    }
}
