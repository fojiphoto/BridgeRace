using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Draglift : MonoBehaviour
{
    public GameObject Lift;

    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent navAgent = other.gameObject.GetComponent<NavMeshAgent>();
        Animator animator = other.gameObject.GetComponent<Animator>();

        if (navAgent != null && animator != null)
        {
            DisableAllScripts(other.gameObject);
            navAgent.enabled = false; // Disable NavMeshAgent to allow manual movement
            animator.SetTrigger("Idle");
        }

        // Disable all scripts on the other game object
        other.transform.SetParent(Lift.transform);
        Invoke("Move_up", 2.2f);
    }

    public void Move_up()
    {
        MoveObject(Lift, Vector3.up * 10.5f, 5f);
    }
  
    private void MoveObject(GameObject obj, Vector3 direction, float duration)
    {
        // Check if the object exists and move it in the specified direction
        if (obj != null)
        {
            obj.transform.DOMove(obj.transform.position + direction, duration);
        }
    }

    public void DisableAllScripts(GameObject obj)
    {
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}
