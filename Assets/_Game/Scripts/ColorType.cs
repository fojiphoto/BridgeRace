using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorType;

public class ColorType : MonoBehaviour
{
    public enum Colortype
    {
        Blue = 0,
        Green = 1,
        Pink = 2,
    }

    [SerializeField] public Colortype colorType;

    public void SetRandomColorIndex()
    {
        colorType = (Colortype)Random.Range(0, 3);
    }

    public void SetColorIndex(int index)
    {
        colorType = (Colortype)index;
    }

    public int IndexColorType() {
        return (int)colorType;
    }
}
