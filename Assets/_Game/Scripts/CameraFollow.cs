using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offset = new Vector3(0, 15, -10);
    Vector3 targetPos;
    // Start is called before the first frame update

    private void Update()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + offset, 0.2f);
        transform.position = smoothedPosition;
    }
}
