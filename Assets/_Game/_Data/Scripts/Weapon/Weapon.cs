using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : GameUnit
{
    public CapsuleCollider myCollider;
    public SpawnWeapon mySpawnWeapon;
    public Transform characterOnHit;

    public bool isOnHit = false;

    [SerializeField] private WeaponSO myWeapon;

    void Start()
    {
        tf = this.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_LAYER_CHAR) && other != myCollider)
        {
            isOnHit = true;
            characterOnHit = other.transform;
        }
    }
    protected void BaseUpdate()
    {
        FlyToDirection();
    }

    protected void FlyToDirection()
    {
        if (myCollider != null)
        {
            tf.position += tf.forward * myWeapon.WeaponSpeed * Time.deltaTime;
        }
    }

    public void SetMyCharacter(CapsuleCollider myCollider, SpawnWeapon mySpawnWeapon)
    {
        this.myCollider = myCollider;
        this.mySpawnWeapon = mySpawnWeapon;
    }

    protected virtual void TypeAttack()
    {

    }
}
