using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingWeapon : Singleton<PoolingWeapon>
{
    [SerializeField] private List<GameObject> weaponPrefabs;
    public List<GameObject> WeaponPrefabs => weaponPrefabs;

    [SerializeField] private Dictionary<GameObject, List<GameObject>> weaponPoolDict = new Dictionary<GameObject, List<GameObject>>();
    [SerializeField] private Transform parent;

    private void Start()
    {
        if (weaponPrefabs.Count > 0)
        {
            for (int i = 0; i < weaponPrefabs.Count; i++)
            {
                List<GameObject> weaponPool = new List<GameObject>();

                for (int j = 0; j < 10; j++)
                {
                    GameObject weapon = Instantiate(weaponPrefabs[i], parent);
                    weapon.transform.SetParent(parent);
                    weapon.SetActive(false);

                    weaponPool.Add(weapon);
                }

                weaponPoolDict.Add(weaponPrefabs[i], weaponPool);
            }
        }
    }

    public GameObject Spawn(Vector3 pos, Quaternion rot, GameObject weaponPrefab)
    {
        GameObject weapon = null;

        for (int i = 0; i < weaponPoolDict[weaponPrefab].Count; i++)
        {
            if (!weaponPoolDict[weaponPrefab][i].activeInHierarchy)
            {
                weapon = weaponPoolDict[weaponPrefab][i];
                break;
            }
        }

        if (weapon == null)
        {
            weapon = GameObject.Instantiate(weaponPrefab, parent);
            weaponPoolDict[weaponPrefab].Add(weapon);
        }

        weapon.transform.SetPositionAndRotation(pos, rot);
        weapon.SetActive(true);

        return weapon;
    }

    public void ResetWeapon(GameObject weapon)
    {
        if (weapon != null)
        {
            weapon.transform.SetPositionAndRotation(parent.position, Quaternion.identity);
            weapon.transform.localScale = Vector3.one;
            weapon.SetActive(false);
        }
    }

    public void Collect(GameObject weaponPrefab)
    {
        for (int i = 0; i < weaponPoolDict[weaponPrefab].Count; i++)
        {
            if (weaponPoolDict[weaponPrefab][i].activeInHierarchy)
            {
                weaponPoolDict[weaponPrefab][i].SetActive(false);
            }
        }
    }
}
