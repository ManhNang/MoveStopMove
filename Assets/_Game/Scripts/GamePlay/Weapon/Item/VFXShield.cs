using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXShield : GameUnit
{
    [SerializeField] ParticleSystem shield;
    public ParticleSystem Shield => shield;
}
