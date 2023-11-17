using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    float delayTime = 1f;

    public void OnEnter(EnemyController enemy)
    {
        enemy.EnemyAttack(delayTime);
        timer = 0;
    }

    public void OnExecute(EnemyController enemy)
    {
        timer += Time.deltaTime;

        if (timer >= delayTime + 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(EnemyController enemy)
    {
        
    }
}
