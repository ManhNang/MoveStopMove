using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponShopData", menuName = "ScriptableObjects/WeaponShopData", order = 1)]
public class WeaponShopData : ScriptableObject
{
    [SerializeField] List<WeaponItem> weaponsItem;
    public int weaponsCount => weaponsItem.Count;
    public List<WeaponItem> WeaponsItem => weaponsItem;
}

[System.Serializable]
public class WeaponItem
{
    public string name;
    public WeaponType weaponType;
    public Weapon prefabs;
    public int cost;
}
