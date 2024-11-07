using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class stepsr_Bridge : MonoBehaviour
{
    public AIcontroller AIcontroller;
    public PLayerController pLayer;
    public Material player;
    public GameObject bag;
    public string name;
    public List<Collider> c1;
    RaycastHit hit;
    void Start()
    {
        AIcontroller = transform.parent.root.gameObject.GetComponent<AIcontroller>();
        player = transform.parent.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        bag = transform.parent.GetChild(2).gameObject;
    }
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 5, Color.yellow);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "EmptyStep")
                {
                    GameObject a = hit.collider.gameObject;
                    MeshRenderer meshRenderer = a.GetComponent<MeshRenderer>();
                    if (bag.transform.childCount > 0 && (!meshRenderer.enabled || meshRenderer.material.color != player.color))
                    {
                        meshRenderer.enabled = true;
                        int size = bag.transform.childCount-1;
                        if (name == "bot")
                        {
                            meshRenderer.material.color = player.color;
                            GameObject back = bag.transform.GetChild(size).transform.gameObject;
                            if (back.GetComponent<AddMaterials>().paarent == AIcontroller.mytiles)
                            {
                                AIcontroller.MyTargets.Add(back);
                            }
                            bag.transform.GetChild(size).GetComponent<AddMaterials>().BackBrgR_ToFirstPosition();
                        }
                        else
                        {
                            if (Input.GetMouseButton(0))
                            {
                                meshRenderer.material.color = pLayer.Bag.transform.GetChild(size).gameObject.GetComponent<MeshRenderer>().material.color;
                                pLayer.Bag.transform.GetChild(size).GetComponent<AddMaterials>().BackBrgR_ToFirstPosition();
                            }
                        }
                    }
                    if (name != "bot")
                    {
                        if (Input.GetMouseButton(0))
                        {
                            if (meshRenderer.material.color == player.color)
                            {
                                a.GetComponents<BoxCollider>()[0].enabled = false;
                            }
                        }
                        if (bag.transform.childCount == 0 && (meshRenderer.material.color != player.color || meshRenderer.material.color == player.color))
                        {
                            if (!c1.Contains(a.GetComponents<BoxCollider>()[0]))
                            {
                                c1.Add(a.GetComponents<BoxCollider>()[0]);
                            }
                        }
                    }
                    if (bag.transform.childCount == 0)
                    {
                        if (name == "bot")
                        {
                            AIcontroller.stepBrgR_sover();
                        }
                    }
                }
            }
        }
    }
    public void buildBrgR_again()
    {
        for (int i = 0; i < c1.Count; i++)
        {
            c1[i].enabled = true;
        }
        c1.Clear();
    }
}
