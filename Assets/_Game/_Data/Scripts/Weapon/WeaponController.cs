using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WeaponController : Singleton<WeaponController>
{
    private IWeaponAttack weapon;

    public void RunAttack()
    {
        weapon?.AttackMechanism();
    }

    public void SetWeapon(WeaponType weaponType)
    {
        string weaponTypeName = weaponType.ToString();
        Type type = Type.GetType(weaponTypeName);

        if (type != null && type.GetInterface(nameof(IWeaponAttack)) != null)
        {
            this.weapon = (IWeaponAttack)Activator.CreateInstance(type);
        }
    }
}