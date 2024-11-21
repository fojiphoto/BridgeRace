using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Transfer : MonoBehaviour
{
    public GameObject Front_Lgate, Front_Rgate;
    public float moveDistance = 3f;

    private Vector3 originalPosObj1;
    private Vector3 originalPosObj2;
    public Transform drag_point;

    public GameObject Lift;
    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent navAgent = other.gameObject.GetComponent<NavMeshAgent>();
        Animator animator = other.gameObject.GetComponent<Animator>();
        originalPosObj1 = Front_Lgate.transform.position;
        originalPosObj2 = Front_Rgate.transform.position;
        // Move obj1 to the left (negative x-axis direction)
        MoveObject(Front_Lgate, Vector3.left * moveDistance, 1f);
        // Move obj2 to the right (positive x-axis direction)
        MoveObject(Front_Rgate, Vector3.right * moveDistance, 1f);
        other.gameObject.transform.DOMove(drag_point.transform.position, 1f).OnComplete(() =>
        {
            if (navAgent != null)
            {
                EnableAllScripts(other.gameObject);
                navAgent.enabled = true; // Re-enable NavMeshAgent after the tween
            }
        });
        other.transform.SetParent(null);
        Invoke("MoveObjectsBack", 0.1f);
        Invoke("Move_down", 2.5f);

    }
    private void MoveObject(GameObject obj, Vector3 direction, float duration)
    {
        // Check if the object exists and move it in the specified direction
        if (obj != null)
        {
            obj.transform.DOMove(obj.transform.position + direction, duration);
        }
    }
    private void MoveObjectsBack()
    {
        // Move obj1 back to its original position
        MoveObject(Front_Lgate, originalPosObj1 - Front_Lgate.transform.position,1f);
        // Move obj2 back to its original position
        MoveObject(Front_Rgate, originalPosObj2 - Front_Rgate.transform.position, 1f);
    }

    public void EnableAllScripts(GameObject obj)
    {
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }

    public void Move_down()
    {
        MoveObject(Lift, Vector3.down * 10.5f, 2.5f);
    }

 
}
