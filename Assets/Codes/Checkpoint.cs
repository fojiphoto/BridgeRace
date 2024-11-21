using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Bot"
        if (collision.gameObject.CompareTag("Bot"))
        {
            // Get the Rigidbody component of the colliding object
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Enable Z-axis movement by removing the Z constraint
                rb.constraints &= ~RigidbodyConstraints.FreezePositionZ; // Remove Z constraint
            }
        }
    }
}
