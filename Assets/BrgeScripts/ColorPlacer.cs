using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ColorPlacer : MonoBehaviour
{
    int totalPlayers;
    private void Awake()
    {
        totalPlayers = cameramovement.Instance.allplayers.Count;
    }
    private void Start()
    {
       totalPlayers= cameramovement.Instance.allplayers.Count;
        Debug.Log("Hello "+totalPlayers);
    }
    public void assigncolor(GameObject player, string name)
    {
        Material temp = player.transform.GetChild(0).transform.GetComponent<SkinnedMeshRenderer>().material;
        int perPlayerTiles=120/totalPlayers;
        for (int j = 0; j < perPlayerTiles; j++)
        {
            int k = Random.Range(0, transform.childCount);
            MeshRenderer m1 = transform.GetChild(k).transform.GetComponent<MeshRenderer>();
            m1.material = temp;
            m1.enabled = true;
            if (name == "bot")
            {
                player.GetComponent<AIcontroller>().MyTargets.Add(transform.GetChild(k).transform.gameObject);
            }
            else if (name == "player")
            {
                player.GetComponent<PLayerController>().MyTargets.Add(transform.GetChild(k).transform.gameObject);
            }
            transform.GetChild(k).parent = null;
        }
    }
}
