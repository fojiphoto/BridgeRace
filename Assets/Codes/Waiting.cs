using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting : MonoBehaviour
{
    public GameObject obj1; // Obj1 ko assign karein
    public Gates gate;
    private Vector3 initialPosition;

    void Start()
    {
        // Initial position ko store karein
        initialPosition = obj1.transform.position;
        gate = GetComponent<Gates>();
    }

    void Update()
    {
        // Check karein agar obj1 apni initial position se hila
        if (obj1.transform.position != initialPosition)
        {
            if(gate != null) 
            {
                gate.Blockage.SetActive(true); // obj2 ko active karein
            }     
        }
        else
        {
            if (gate != null)
            {
                gate.Blockage.SetActive(false); // obj2 ko inactive karein
            }

        }
    }


}

