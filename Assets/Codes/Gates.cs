using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public GameObject Front_Lgate, Front_Rgate;
    public float moveDistance = 3f;

    private Vector3 originalPosObj1;
    private Vector3 originalPosObj2;

    public GameObject Blockage;
    public GameObject Lift;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosObj1 = Front_Lgate.transform.position;
        originalPosObj2 = Front_Rgate.transform.position;
        initialPosition = Lift.transform.position;
    }

    public void Update()
    {
        if(Lift.transform.position != initialPosition)
        {
            Blockage.SetActive(true);
        }
        else
        {
            Blockage.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Freeze the Z-axis position
                rb.constraints |= RigidbodyConstraints.FreezePositionZ; // Add Z constraint
            }
        }

        Blockage.SetActive(true);

        Invoke("moveobjects", 0.05f);

        Invoke("MoveObjectsBack", 0.3f);
    }

    private void MoveObject(GameObject obj, Vector3 direction, float duration)
    {
        if (obj != null)
        {
            obj.transform.DOMove(obj.transform.position + direction, duration);
        }
    }

    private void moveobjects()
    {
        MoveObject(Front_Lgate, Vector3.left * moveDistance, 2f);
        MoveObject(Front_Rgate, Vector3.right * moveDistance, 2f);
    }

    private void MoveObjectsBack()
    {
        MoveObject(Front_Lgate, originalPosObj1 - Front_Lgate.transform.position, 2f);
        MoveObject(Front_Rgate, originalPosObj2 - Front_Rgate.transform.position, 2f);
    }
}
