using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinVictim : GameUnit
{
    [SerializeField] GameObject child;
    public void SetActivePin(Player player)
    {
        Character victim = player.DectectVictimNearest();

        if (victim != null && !victim.IsDead && !player.IsDead)
        {
            TF.position = victim.TF.position;
            TF.rotation = Quaternion.identity;
            child.SetActive(true);
        }
        else
        {
            child.SetActive(false);
        }
    }

    public void SetActive(bool active)
    {
        child.SetActive(active);
    }
}
