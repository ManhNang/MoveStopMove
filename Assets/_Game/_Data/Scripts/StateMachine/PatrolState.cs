using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private float randomTime;
    private float timer;

    public void OnEnter(EnemyController enemy)
    {
        timer = 0;
        randomTime = Random.Range(5f, 7f);
    }

    public void OnExecute(EnemyController enemy)
    {
        timer += Time.deltaTime;

        if (GameManager.Instance.IsState(GameState.Setting))
        {
            enemy.ChangeState(new IdleState());
        }

        else if (enemy.IsTargetInRange() && timer > 2f)
        {
            enemy.ChangeState(new IdleState());
        }

        else if (timer < randomTime)
        {
            enemy.Moving();
        }

        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(EnemyController enemy)
    {

    }
}
