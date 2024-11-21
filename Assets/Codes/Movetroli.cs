using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Movetroli : MonoBehaviour
{
    public Transform troli;
    public Vector3 end_pos;
    public Transform first_point;
    public Vector3 start_pos;
    public PLayerController PLayerController;
    public List<AIcontroller> aiControllers = new List<AIcontroller>();

    private void Start()
    {
        start_pos = troli.transform.position;
        // Get the PLayerController from the player GameObject
        //GameObject player = GameObject.Find("Player");
        //if (player != null)
        //{

        //    if (PLayerController != null)
        //    {
        //        Debug.Log("PlayerController found on Player GameObject.");
        //    }
        //    else
        //    {
        //        Debug.LogError("PlayerController component not found on Player GameObject.");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Player GameObject with tag 'Player' not found.");
        //}

        // Get all AIcontrollers from the GameObjects tagged with "Bot"
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
        foreach (GameObject bot in bots)
        {
            AIcontroller aiController = bot.GetComponent<AIcontroller>();
            if (aiController != null)
            {
                aiControllers.Add(aiController);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MonoBehaviour Script = other.gameObject.GetComponent<MonoBehaviour>();
        NavMeshAgent navAgent = other.gameObject.GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.enabled = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            PLayerController = other.gameObject.GetComponent<PLayerController>();
            if (PLayerController != null)
            {
                PLayerController.sendallbrikesback();
            }
        }
        if (other.gameObject.CompareTag("Bot"))
        {
            AIcontroller aiController = other.GetComponent<AIcontroller>();
            if (aiController != null)
            {
                aiController.sendallbrikesback();
            }
        }
        other.transform.position = first_point.transform.position;
        other.transform.SetParent(troli.transform);
        other.gameObject.transform.DOMove(first_point.transform.position, 1f).OnComplete(() =>
        {
            Script.enabled = false;
            troli.gameObject.transform.DOMove(end_pos, 5f).OnComplete(() =>
            {
                other.transform.SetParent(null);
                other.gameObject.transform.DOMoveZ(other.transform.position.z + 3f, 1f);
                navAgent.enabled = true;
                Script.enabled = true;
                Invoke("move_back", 2f);
            });
        });
    }

    public void move_back()
    {
        troli.gameObject.transform.DOMove(start_pos, 1f);
    }

}

