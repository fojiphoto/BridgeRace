using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Slide : MonoBehaviour
{
    public Transform end_point;
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
                animator.SetTrigger("Zipline"); // Trigger the animation

                // Move the other object 12 meters forward in the Z direction
                other.gameObject.transform.DOMove(end_point.transform.position, 1.5f).OnComplete(() =>
                {
                    //animator.ResetTrigger("Zipline"); 
                    if (navAgent != null)
                    {
                        navAgent.enabled = true; // Re-enable NavMeshAgent after the tween
                        animator.SetTrigger("Run");
                    }
                    // Optional: You can add any logic here to execute after the tween is complete.
                    // hasTriggered = false; // If you want to allow retriggering, uncomment this line.
                });
            }
        }
    }

