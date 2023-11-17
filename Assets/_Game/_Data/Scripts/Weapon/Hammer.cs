using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private Transform hammerImg;
    private float rotationSpeed = 1000f;
    void Update()
    {
        BaseUpdate();
        TypeAttack();
    }

    protected override void TypeAttack()
    {
        base.TypeAttack();
        hammerImg.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
