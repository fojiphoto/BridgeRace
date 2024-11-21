using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlacer : MonoBehaviour
{
    public int color_number;
    public void assigncolor(GameObject player, string name)
    {
        Material temp = player.transform.GetChild(0).transform.GetComponent<SkinnedMeshRenderer>().material;
        for (int j = 0; j < color_number; j++)
        {
            int k = Random.Range(0, transform.childCount);
            MeshRenderer m1 = transform.GetChild(k).transform.GetComponent<MeshRenderer>();
            m1.material = temp;
            m1.enabled = true;
            //transform.GetChild(k).GetComponent<ParticleSystem>().startColor = temp.color;
            if (name == "bot")
            {
                transform.GetChild(k).transform.gameObject.name = "bot";
                player.GetComponent<AIcontroller>().MyTargets.Add(transform.GetChild(k).transform.gameObject);
            }
            else if (name == "player")
            {
                transform.GetChild(k).transform.gameObject.name = "player";
                player.GetComponent<PLayerController>().MyTargets.Add(transform.GetChild(k).transform.gameObject);

            }
            transform.GetChild(k).parent = null;
        }
    }
}
