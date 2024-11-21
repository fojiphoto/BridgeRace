using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    public Animator animator;
    public bool alive;
    public Material MyMaterial;
    public GameObject Bag;
    public GameObject blockinbag;
    public GameObject mytiles;
    public List<GameObject> mytargetparents;
    public List<GameObject> MyTargets;
    public int stage;
    public int exits;
    bool move;
    public float moveDuration = 0.3f;
    public Ease moveEase = Ease.OutQuad;
    public AudioClip Pick_up;
    public AudioSource audio_source;

    void Start()
    {
        MyMaterial = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        Bag = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();
        alive = true;
        exits = 0;
        stage = 0;
        keepbricks();
        move = true;
    }

    void Update()
    {
        if (move)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Run");
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetTrigger("Idle");
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && move)
        {
            if (alive)
            {
                Vector3 direction = Vector3.forward * Input.GetAxis("Mouse Y") + Vector3.right * Input.GetAxis("Mouse X");
                transform.Translate(Vector3.forward * 6 * Time.deltaTime);

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 8.5f * Time.deltaTime);
                }
            }
        }
    }

    public void sendallbrikesback()
    {
        int tempsize = Bag.transform.childCount;
        for (int i = 0; i < tempsize; i++)
        {
            Bag.transform.GetChild(0).GetComponent<AddMaterials>().BackToFirstPosition();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "gate")
        {
            other.gameObject.tag = "Untagged";
            other.gameObject.transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;
            stage++;
            if (stage <= 2)
            {
                keepbricks();
            }
        }
        if (other.gameObject.tag == "0th")
        {
            exits++;
            if (exits == 2)
            {
                exits = 0;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            if (Bag.transform.childCount != 0 && exits == 0)
            {
                if (collision.gameObject.transform.GetComponent<AIcontroller>().Bag.transform.childCount > Bag.transform.childCount)
                {
                    StartCoroutine(stand());
                    Vibration.Vibrate(100);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cude"))
        {
            if (move && IsCorrectBrick(other))
            {
                GameObject brick = other.gameObject;
                audio_source.PlayOneShot(Pick_up);

                // Immediately parent the brick to the Bag to follow the player's movement
                brick.transform.SetParent(Bag.transform);

                // Start the coroutine to smoothly move the brick to the correct stacking position
                StartCoroutine(SmoothMoveToBag(brick));
            }
        }
        else if (other.gameObject.CompareTag("BrickBuster"))
        { 
            GameObject newBrick = other.gameObject;
            // Handle BrickBuster trigger: add 5 bricks
            for (int i = 0; i < 5; i++)
            {
               GameObject NewBricks = Instantiate(newBrick, other.transform.position, Quaternion.identity);
                NewBricks.GetComponent<MeshRenderer>().material = MyMaterial; // Match player's material
                audio_source.PlayOneShot(Pick_up);

                // Parent each new brick to Bag and apply the same stacking animation
                NewBricks.transform.SetParent(Bag.transform);
                StartCoroutine(SmoothMoveToBag(NewBricks));
            }
        }

        if (other.gameObject.tag == "nth")
        {
            other.transform.root.GetComponent<Eliminater>().thischar(gameObject);
        }
    }

    private IEnumerator SmoothMoveToBag(GameObject brick)
    {
        Vector3 targetPosition = new Vector3(0, Bag.transform.childCount * 0.25f, 0);

        brick.transform.DOLocalJump(targetPosition, 2.5f, 1, 0.4f)
            .SetEase(moveEase)
            .OnComplete(() =>
            {
                AttachToBag(brick);
            });

        while (brick.transform.localPosition != targetPosition)
        {
            brick.transform.localPosition = Vector3.Lerp(brick.transform.localPosition, targetPosition, Time.deltaTime * 5f);
            yield return null;
        }
    }

    private bool IsCorrectBrick(Collider other)
    {
        return other.gameObject.GetComponent<MeshRenderer>().material.color == MyMaterial.color;
    }

    private Vector3 BagChildPos()
    {
        if (Bag.transform.childCount > 0)
        {
            Transform lastBrick = Bag.transform.GetChild(Bag.transform.childCount - 1);
            return lastBrick.position + new Vector3(0, 0.25f, 0);
        }
        else
        {
            return Bag.transform.position;
        }
    }

    private void AttachToBag(GameObject brick)
    {
        brick.transform.localPosition = new Vector3(0, Bag.transform.childCount * 0.25f, 0);
        brick.transform.localRotation = Quaternion.identity;
    }

    public void keepbricks()
    {
        mytiles = mytargetparents[stage].gameObject;
        mytargetparents[stage].GetComponent<ColorPlacer>().assigncolor(gameObject, "player");
    }

    IEnumerator stand()
    {
        animator.SetTrigger("knock");
        move = false;
        GetComponent<Collider>().isTrigger = true;
        sendallbrikesback();
        yield return new WaitForSeconds(0.6f);
        GetComponent<Collider>().isTrigger = false;
        move = true;
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
}
