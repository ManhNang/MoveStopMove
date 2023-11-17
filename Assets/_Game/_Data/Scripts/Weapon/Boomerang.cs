using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    [SerializeField] private Transform boomerangImg;
    private float rotationSpeed = 2000f;
    void Update()
    {
        BaseUpdate();
        TypeAttack();
    }

    protected override void TypeAttack()
    {
        base.TypeAttack();
        boomerangImg.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
