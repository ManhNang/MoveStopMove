using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Hammer,
    Boomerang,
    Knife
}

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/Weapon")]

[Serializable]
public class WeaponSO : ScriptableObject
{
    public WeaponController weaponController;

    [SerializeField] private WeaponType weaponType;
    public WeaponType WeaponType => weaponType;

    [SerializeField] private int weaponSize;

    [SerializeField] private float weaponSpeed;
    public float WeaponSpeed => weaponSpeed;

    public void TestBug()
    {
        //weaponController.
    }
}
