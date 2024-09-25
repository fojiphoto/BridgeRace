using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class BrickBridge : MonoBehaviour
{
    [SerializeField] private ColorTypeSO colorTypeS0;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] ColorType colorType;
    int indexColor;

    private void Start()
    {
        OnInit();
        Debug.Log("BrickOnBridge" + indexColor);
    }

    //Hàm khởi tạo
    void OnInit()
    {
        //Tắt visual
        mesh.enabled = false;
        //Set tag tạm thời chưa có màu
        gameObject.tag = "BrickOnBridge";
        indexColor = 99;
        colorType.SetColorIndex(indexColor);
    }

}
