using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnWeapon : GameUnit
{
    [SerializeField] private CapsuleCollider myCollider;

    [SerializeField] private GameObject myWeapon;

    [SerializeField] private List<GameObject> bulletsUsing;

    private Character myCharacter;
    public List<GameObject> BulletUsing { get => bulletsUsing; set { bulletsUsing = value; } }

    void Start()
    {
        tf = this.transform;
    }

    public void OnInit()
    {
        myCharacter = myCollider.GetComponent<Character>();
        List<GameObject> listWeapon = PoolingWeapon.Instance.WeaponPrefabs;
        myWeapon = listWeapon[Random.Range(0, listWeapon.Count)];
    }

    public void SpawnWeaponFromPool()
    {
        if (myCollider != null) 
        {
            Vector3 posSpawn = tf.position;
            posSpawn.y = 2f;
            GameObject weapon = PoolingWeapon.Instance.Spawn(posSpawn, tf.rotation, myWeapon);
            weapon.GetComponent<Weapon>().SetMyCharacter(myCollider, this);
            weapon.transform.localScale *= myCharacter.Size;
            bulletsUsing.Add(weapon);
        }
    }
}
