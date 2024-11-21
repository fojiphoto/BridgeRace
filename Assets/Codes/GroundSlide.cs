using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundSlide : MonoBehaviour
{
    public Transform Slide_end;
    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent navAgent = other.gameObject.GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.enabled = false; // Disable NavMeshAgent to allow manual movement
        }

        Animator animator = other.gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Slide"); // Trigger the animation

            // Move the other object 12 meters forward in the Z direction
            other.gameObject.transform.DOMove(Slide_end.transform.position, 1.5f).OnComplete(() =>
            {
                //animator.ResetTrigger("Zipline"); 
                if (navAgent != null)
                {
                    navAgent.enabled = true; // Re-enable NavMeshAgent after the tween

                    // Set animation trigger based on the name of the gameObject
                    if (other.gameObject.CompareTag("Bot"))
                    {
                        animator.SetTrigger("Run");
                    }
                    if (other.gameObject.CompareTag("Player"))
                    {
                        animator.SetTrigger("Idle");
                    }
                    //else if (other.gameObject.CompareTag("Bot"))
                    //{
                    //    animator.SetTrigger("Run");
                    //}
                }
                // Optional: You can add any logic here to execute after the tween is complete.
                // hasTriggered = false; // If you want to allow retriggering, uncomment this line.
            });
        }
    }
}
