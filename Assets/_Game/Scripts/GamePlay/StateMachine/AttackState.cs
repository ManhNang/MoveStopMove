using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.OnAttack();
        t.Counter.Start(
            () =>
            {
                if (t.VictimNearest != null && !t.VictimNearest.IsDead && t.CanAttack() && Mathf.Abs(Camera.main.WorldToViewportPoint(t.TF.position).x) < 1f && Mathf.Abs(Camera.main.WorldToViewportPoint(t.TF.position).y) < 1f)
                {
                    t.CurrentSkin.CurrentWeapon.Throw(t, t.VictimNearest, t.PosAttackPoint, t.OnHitVictim);
                }
                t.Counter.Start(() => t.RandomStateAfterAttack(), 2f);
            }, Constant.DELAY_TIME_THROW);
    }
    public void OnExecute(Bot t)
    {
        t.Counter.Execute();
    }
    public void OnExit(Bot t)
    {

    }
}
