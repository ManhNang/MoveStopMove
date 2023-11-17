using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float randomTime;
    float timer;

    public void OnEnter(EnemyController enemy)
    {
        enemy.StopMoving();

        timer = 0;
        randomTime = Random.Range(1f, 2f);
    }

    public void OnExecute(EnemyController enemy)
    {
        timer += Time.deltaTime;

        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else if (timer > randomTime)
            {
                enemy.ChangeState(new PatrolState());
            }
        }
    }

    public void OnExit(EnemyController enemy)
    {

    }
}
