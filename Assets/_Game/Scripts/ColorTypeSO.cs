using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorType", menuName = "ScriptableObjects/ColorType", order = 1)]
public class ColorTypeSO : ScriptableObject
{
    [SerializeField] public List<Material> Materials = new List<Material>();
}
