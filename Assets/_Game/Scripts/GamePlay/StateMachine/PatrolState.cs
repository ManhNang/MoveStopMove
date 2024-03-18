using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constant.ANIM_RUN);
        t.Moving();
    }
    public void OnExecute(Bot t)
    {
        if (t.IsDestination)
        {
            t.ChangeState(new IdleState());
        }
    }
    public void OnExit(Bot t)
    {

    }
}
