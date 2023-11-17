using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    [SerializeField] private Transform knifeImg;
    //private float rotationSpeed = 100;
    void Update()
    {
        BaseUpdate();
    }
}
