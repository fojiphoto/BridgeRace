using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AIcontroller : MonoBehaviour
{
    public bool collection, placeing, movetoplace, backdown;
    public int carethismany, havethismany;
    public GameObject othstep, nthstep;
    public GameObject mytiles;
    public List<GameObject> mytargetparents;
    public List<GameObject> MyTargets;
    public List<GameObject> mystairs;
    public List<GameObject> staircase;
    public GameObject Thisstaircase;
    public GameObject TargetTile;
    public Material MyMaterial;
    public Animator animator;
    public GameObject Bag;
    public int total, count;
    public int stage;
    public float speed;
    public int exits;
    public bool move;
    void Start()
    {
        havethismany = total = count = stage = 0;
        move = true;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Run");
        Bag = transform.GetChild(2).gameObject;
        MyMaterial = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        TakeBrgR_thismany();
        addBrgR_stairscases();
        collection = true;
        GoToBrgR_Tile();
        exits = 0;
    }
    void Update()
    {
        if (move)
        {
            if (collection)
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetTile.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, TargetTile.transform.position) < 0.2f)
                { GetComponent<Collider>().isTrigger = false; }
            }
            if (placeing)
            {
                transform.position = Vector3.MoveTowards(transform.position, othstep.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, othstep.transform.position) < 0.15f)
                {
                    placeing = false;
                    movetoplace = true;
                    transform.LookAt(nthstep.transform.position);
                }
            }
            if (movetoplace)
            {
                transform.position = Vector3.MoveTowards(transform.position, nthstep.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, nthstep.transform.position) < 0.25f)
                {
                    nthstep.transform.root.GetComponent<Eliminater>().thischar(gameObject);
                    movetoplace = false;
                    stage++;
                    TakeBrgR_thismany();
                    addBrgR_stairscases();
                    GoToBrgR_Tile();
                    GetComponent<Collider>().isTrigger = false;
                    exits = 0;
                }
            }
            if (backdown)
            {
                transform.position = Vector3.MoveTowards(transform.position, othstep.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, othstep.transform.position) < 0.25f)
                {
                    backdown = false;
                    TakeBrgR_thismany();
                    GoToBrgR_Tile();
                    collection = true;

                }
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cude"))
        {
            if (collision.gameObject.GetComponent<MeshRenderer>().material.color == MyMaterial.color)
            {
                GameObject a = collision.gameObject;
                a.transform.parent = Bag.transform;
                a.transform.localPosition = new Vector3(Bag.transform.localPosition.x, (Bag.transform.childCount * 0.25f), Bag.transform.localPosition.z);
                a.transform.localRotation = Quaternion.Euler(0, 0, 0);
                MyTargets.Remove(collision.gameObject);
                if (!placeing)
                {
                    havethismany++;
                    if (carethismany == havethismany)
                    {
                        ChooseBrgR_ACase();
                    }
                    else
                    {
                        GoToBrgR_Tile();
                    }
                }
            }
        }

        if (collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Player"))
        {
            if (!movetoplace)
            {
                if (Bag.transform.childCount > 0)
                {
                    if (collision.gameObject.CompareTag("Bot"))
                    {
                        if (collision.gameObject.transform.GetComponent<AIcontroller>().Bag.transform.childCount > Bag.transform.childCount)
                        {
                            StartCoroutine(BrgR_stand());
                        }
                    }
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        if (collision.gameObject.transform.GetComponent<PLayerController>().Bag.transform.childCount > Bag.transform.childCount)
                        {
                            StartCoroutine(BrgR_stand());
                        }
                    }
                }
            }
        }
    }
    public void TakeBrgR_thismany()
    {
        havethismany = 0;
        carethismany = Random.Range(8, 20);
    }
    IEnumerator BrgR_stand()
    {
        GetComponent<Collider>().isTrigger = true;
        animator.SetTrigger("knock");
        move = false;
        sendallBrgR_brikesback();
        yield return new WaitForSeconds(1.2f);
        move = true;
        animator.SetTrigger("Run");
        GetComponent<Collider>().isTrigger = false;
    }
    public void sendallBrgR_brikesback()
    {
        int tempsize = Bag.transform.childCount;
        for (int i = 0; i < tempsize; i++)
        {
            MyTargets.Add(Bag.transform.GetChild(0).gameObject);
            Bag.transform.GetChild(0).GetComponent<AddMaterials>().BackBrgR_ToFirstPosition();
        }
    }
    public void ChooseBrgR_ACase()
    {
        total = 0;
        count = 0;
        for (int i = 0; i < staircase.Count; i++)
        {
            count = 0;
            GameObject k = staircase[i].transform.GetChild(1).gameObject;
            for (int j = 0; j < k.transform.childCount; j++)
            {
                MeshRenderer thism = k.transform.GetChild(j).GetComponent<MeshRenderer>();
                if (thism.enabled)
                {
                    if (thism.material.color == MyMaterial.color)
                    {
                        count++;
                        if (count > total)
                        {
                            total = count;
                            Thisstaircase = staircase[i];
                        }
                    }
                }
                else
                {
                    break;
                }
            }

        }
        if (total == 0)
        {
            Thisstaircase = staircase[Random.Range(0, staircase.Count)];
            BrgR_andnth();
        }
        else
        {
            BrgR_andnth();
        }
        transform.LookAt(othstep.transform.position);
    }
    public void stepBrgR_sover()
    {
        movetoplace = collection = placeing = false;
        backdown = true;
        transform.LookAt(othstep.transform.position);
    }
    public void GoToBrgR_Tile()
    {
        float low = 100f;
        GetComponent<Collider>().isTrigger = false;
        for (int i = 0; i < MyTargets.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, MyTargets[i].transform.position);
            if (low > distance)
            {
                low = distance;
                TargetTile = MyTargets[i];
            }
        }
        transform.LookAt(TargetTile.transform);
    }
    public void BrgR_andnth()
    {
        transform.LookAt(Thisstaircase.transform.position);
        othstep = Thisstaircase.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        nthstep = Thisstaircase.transform.GetChild(1).GetChild((Thisstaircase.transform.GetChild(1).childCount) - 1).GetChild(0).gameObject;
        collection = false;
        placeing = true;
    }
    public void addBrgR_stairscases()
    {
        if (stage <= 2)
        {
            staircase.Clear();
            MyTargets.Clear();
            mytiles = mytargetparents[stage].gameObject;
            mytargetparents[stage].GetComponent<ColorPlacer>().assigncolor(gameObject, "bot");
            for (int i = 0; i < mystairs[stage].transform.childCount; i++)
            {
                staircase.Add(mystairs[stage].transform.GetChild(i).gameObject);
            }
            collection = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "0th")
        {
            exits++;
            if (exits == 1)
            {
                GetComponent<Collider>().isTrigger = true;
            }
            if (exits == 2)
            {
                GetComponent<Collider>().isTrigger = false;
                exits = 0;
            }
        }

    }
}
