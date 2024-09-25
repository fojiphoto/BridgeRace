using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //model 3D
    [SerializeField] protected GameObject model;
    //Prefabs brick sau lưng
    [SerializeField] protected GameObject brickOnBack;
    //Điểm sinh ra brick
    [SerializeField] protected Transform pointSpawnBrick, index;
    //Màu của player
    [SerializeField] protected SkinnedMeshRenderer playerColor;
    [SerializeField] protected MeshRenderer brickOnBackColor;
    [SerializeField] public Animator anim;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;
    protected int currBrickOnBack = 0;
    int playerIndexColor;

    //List sẽ chứa brick sau lưng + (Object Pooling)
    List<GameObject> brickList = new List<GameObject>();
    [SerializeField] protected ColorTypeSO colorTypeS0;
    //Kiểm tra xem player có đang di chuyển xuống(mục đích khi player di chuyển xuống cầu thì sẽ không đổi màu gạch ở trên cầu)
    protected bool isMovingDown;
    //Set trạng thái player có thể di chuyển hay không(mục đích không thể đi lên cầu khi hết gạch)
    protected bool canMove = true, fall = false;

    [SerializeField] public ColorType colorType;

    //public string brickBridgeTag, brickOnGroundTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BrickOnGround"))
        {
            if (other.gameObject.GetComponent<ColorType>().IndexColorType() == playerIndexColor)
            {
                AddBrick();
            }
            else
            {
                return;
            }
        }
        else if (other.gameObject.CompareTag("BrickOnBridge"))
        {
            if (other.gameObject.GetComponent<ColorType>().IndexColorType() != playerIndexColor && !isMovingDown)
            {
                RemoveBrick();
                MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
                mesh.material = colorTypeS0.Materials[playerIndexColor];
                other.gameObject.GetComponent<ColorType>().SetColorIndex(playerIndexColor);
                if (mesh.enabled == true)
                {
                    return;
                }
                else
                {
                    mesh.enabled = true;
                }
            }
            else
            {
                return;
            }
        }
        else if(other.gameObject.CompareTag("CheckStage"))
        {
            ActionManager.OnNewStage?.Invoke(colorType.IndexColorType());
            Debug.Log("CheckStage");
        }
        else
        {
            return;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            int otherbirck = collision.gameObject.GetComponent<Player>().CheckCurrBrickOnBack();
            if (currBrickOnBack < otherbirck)
            {
                VarNhau();
                Debug.Log("Var player");
            } else if (currBrickOnBack > otherbirck)
            {
                collision.gameObject.GetComponent<Player>().VarNhau();
                Debug.Log("Var player");
            } else
            {
                return;
            }
        }
    }

    //Khởi tạo màu và gắn tag cho player
    protected void OnInitPlayer()
    {
        playerIndexColor = colorType.IndexColorType();
        brickOnBackColor.material = colorTypeS0.Materials[playerIndexColor];
        playerColor.material = colorTypeS0.Materials[playerIndexColor];
    }


    //Innova
    public void VarNhau()
    {
        ColorBrickOnFall();
        fall = true;
        canMove = false;
        Invoke(nameof(WakeUp), 1.56f);
    }

    public void WakeUp()
    {
        ClearBrickOnWake();
        fall = false;
        canMove = true;
    }
    //Hàm cộng brickvisual
    void AddBrick()
    {
        //Tăng brick sau lưng hiện có
        currBrickOnBack++;
        //Trường hợp số lượng brick sau lưng cao nhất thì sẽ sinh ra và thêm vào mảng
        if (currBrickOnBack > brickList.Count)
        {
            //Sinh ra viên mới nhất
            GameObject brick = Instantiate(brickOnBack, index.transform.position, index.transform.rotation, pointSpawnBrick.transform);
            //Tăng vị trí spawn viên tiếp theo
            index.position = index.transform.position + index.transform.up * 0.2f;
            brickList.Add(brick);
        }
        //Khi trong mảng đã có thì sẽ bật lên và không cần phải sinh ra
        else
        {
            //Trỏ đến vị trí viên gần nhất bị xoá
            GameObject brick = brickList[currBrickOnBack - 1];
            //Tăng vị trí spawn viên tiếp theo
            index.position = index.transform.position + index.transform.up * 0.2f;
            brick.SetActive(true);
        }
    }
    //Hàm xoá brickvisual
    public void RemoveBrick()
    {
        //Nếu chưa có gạch thì ko làm gì
        if (currBrickOnBack == 0)
        {
            return;
        }
        else
        {
            //Giảm brick hiện có
            currBrickOnBack--;
            //Trỏ đến viên cao nhất trên lưng
            GameObject brick = brickList[currBrickOnBack];
            //Tắt viên đó
            brick.SetActive(false);
            //Giảm vị trí spawn viên tiếp theo
            index.position = index.transform.position - index.transform.up * 0.2f;
        }
    }

    //Hàm xoá all brickvisual
    void ClearBrick()
    {
        //Nếu chưa có gạch thì ko làm gì
        if (currBrickOnBack == 0)
        {
            return;
        }
        else
        {
            currBrickOnBack = 0;
            foreach (GameObject obj in brickList)
            {
                Destroy(obj);
            }
            brickList.Clear();
            index.position = index.transform.position;
        }
    }

    void ColorBrickOnFall()
    {
        foreach (GameObject obj in brickList)
        {
            obj.GetComponent<MeshRenderer>().material = colorTypeS0.Materials[3];
        }
        index.position = index.transform.position;
    }

    void ClearBrickOnWake()
    {
        index.position = index.transform.position - index.transform.up*0.2f*currBrickOnBack;
        currBrickOnBack = 0;
        foreach (GameObject obj in brickList)
        {
            obj.gameObject.SetActive(false);
        }
        brickList.Clear();
    }

    //Hàm kiểm tra player có thể di chuyển lên cầu hay không
    //Hãy gọi trong hàm Update của script kế thừa
    RaycastHit ray;
    public LayerMask layer;
    protected void CheckBrick()
    {
        //Bắn tia ray về phía trước mặt player
        Physics.Raycast(transform.position + transform.up * 0.4f, transform.forward, out ray, 0.8f, layer);
        Debug.DrawRay(transform.position + transform.up * 0.8f, transform.forward * 0.4f, Color.red);
        if (ray.collider == null)
        {
            return;
        }
        //Khi trên lưng không có gạch và va chạm với brick trên cầu mà chưa từng đi qua hoặc của người khác thì sẽ không thể đi lên tiếp
        else if ((ray.collider.gameObject.GetComponent<ColorType>().IndexColorType() != playerIndexColor) && currBrickOnBack == 0)
        {
            canMove = false;
        } else
        {
            return;
        }
    }

    public int CheckCurrBrickOnBack()
    {
        return currBrickOnBack;
    }


}
