using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Liftup : MonoBehaviour
{
    public PLayerController PLayerController;
    public List<AIcontroller> aiControllers = new List<AIcontroller>();

    private void Start()
    {
        // Get the PLayerController from the player GameObject
        //GameObject player = GameObject.Find("Player");
        //if (player != null)
        //{
        //    PLayerController = player.GetComponent<PLayerController>();
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
        if (other.gameObject.CompareTag("Player"))
        {
            PLayerController = other.gameObject.GetComponent<PLayerController>();

            if (PLayerController != null)
            {
                Debug.Log("Player entered the trigger.");
                PLayerController.sendallbrikesback();
            }
            else
            {
                Debug.LogError("PlayerController is null when Player entered the trigger.");
            }
        }
        else if (other.gameObject.CompareTag("Bot"))
        {
            AIcontroller aiController = other.GetComponent<AIcontroller>();
            if (aiController != null)
            {
                aiController.sendallbrikesback();
            }
        }
    }

}
