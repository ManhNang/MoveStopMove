using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : GameUnit
{
    [SerializeField] Animator anim;
    [SerializeField] Transform head;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] Renderer pant;

    [SerializeField] PantSO pantSO;

    Weapon currentWeapon;
    Accessory currentAccessory;
    Hat currentHat;
    public Weapon CurrentWeapon => currentWeapon;
    public Animator Anim => anim;

    public void ChangeWeapon(WeaponType weaponType)
    {
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        if (accessoryType != AccessoryType.ACC_None)
        {
            currentAccessory = SimplePool.Spawn<Accessory>((PoolType)accessoryType, leftHand);
        }
    }

    public void ChangeHat(HatType hatType)
    {
        if (hatType != HatType.HAT_None)
        {
            currentHat = SimplePool.Spawn<Hat>((PoolType)hatType, head);
        }
    }

    public void ChangePant(PantType pantType)
    {
        pant.material = pantSO.GetMaterial(pantType);
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(currentWeapon);

        if (currentAccessory) SimplePool.Despawn(currentAccessory);
        if (currentHat) SimplePool.Despawn(currentHat);
    }

    public void DespawnHat()
    {
        if (currentHat) SimplePool.Despawn(currentHat);
    }
    public void DespawnAccessory()
    {
        if (currentAccessory) SimplePool.Despawn(currentAccessory);
    }

    internal void DespawnWeapon()
    {
        if (currentWeapon) SimplePool.Despawn(currentWeapon);
    }
}
