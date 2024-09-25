using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Player
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] GameObject rayFindBrick;
    [SerializeField] private Ray ray;
    [SerializeField] private RaycastHit rayhit;
    public LayerMask layerCheckBrickOnGround;
    Vector3 target;
    GameObject checkPoint, endPos;

    private void Start()
    {
        OnInitPlayer();
        target = transform.position;
        checkPoint = GameObject.FindWithTag("CheckPoint" + colorType.IndexColorType());
        endPos = GameObject.FindWithTag("EndPos");
        Debug.Log("Bot Color Index :" + colorType.IndexColorType());
    }

    private void Move()
    {
        navMeshAgent.SetDestination(target);
        anim.SetTrigger("Running");
    }

    private void Stop()
    {
        navMeshAgent.velocity = Vector3.zero;
        anim.SetTrigger("Idle");
        FindBrick();
    }

    private void FindBrick()
    {
        Physics.Raycast(rayFindBrick.transform.position + rayFindBrick.transform.up * 0.2f, rayFindBrick.transform.forward, out rayhit, 20f, layerCheckBrickOnGround);
        rayFindBrick.transform.Rotate(Vector3.up, 500f * Time.deltaTime);
        if(rayhit.collider == null)
        {
            return;
        } else if(rayhit.collider.gameObject.GetComponent<ColorType>().IndexColorType() == colorType.IndexColorType())
        {
            target = rayhit.point;
        }
    }

    private void MoveOnEndPos()
    {
        isMovingDown = false;
        target = endPos.transform.position;
        Move();
    }

    private void MoveOnGround()
    {
        isMovingDown = true;
        canMove = true;
        target = checkPoint.transform.position;
        Move();
    }

    private void Update()
    {
        if(fall)
        {
            anim.SetTrigger("Fall");
            navMeshAgent.velocity = Vector3.zero;
        }
        else
        {
            CheckBrick();
            if (Vector3.Distance(transform.position, target) <= 0.3f)
            {
                Stop();
            }
            else if (currBrickOnBack >= 6)
            {
                MoveOnEndPos();
            }
            else if (!canMove)
            {
                MoveOnGround();
            }
            else
            {
                Move();
            }
        }
    }
}
