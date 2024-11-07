using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public int num;
    // Start is called before the first frame update
    void Start()
    {
       num=Random.Range(1,4);
        this.GetComponent<Animator>().SetInteger("idle", num);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
