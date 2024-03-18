using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.StopMoving();
        float randTime = Random.Range(1f, 3f);
        t.Counter.Start(() => t.ChangeState(new PatrolState()), randTime);
    }
    public void OnExecute(Bot t)
    {
        t.Counter.Execute();

        if (t.CountVictims > 0)
        {
            t.Counter.Cancel();
            t.ChangeState(new AttackState());
        }
    }
    public void OnExit(Bot t)
    {

    }
}
