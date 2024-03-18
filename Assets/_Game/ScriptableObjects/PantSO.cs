using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantSO", menuName = "ScriptableObjects/PantSO", order = 1)]
public class PantSO : ScriptableObject
{
    [SerializeField] Material[] pantMaterials;

    public Material GetMaterial(PantType pantType)
    {
        return pantMaterials[(int)pantType];
    }
}
