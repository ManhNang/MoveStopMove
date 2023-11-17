using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    private IState currentState;

    private Character target;
    public Character Target { get => target; }

    private Vector3 destinationPos;

    [SerializeField] private float radius;
    [SerializeField] private bool debugBool;

    void Start()
    {
        base.OnInit();
        agent.speed = moveSpeed;
        destinationPos = tf.position;
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null && !isDead)
        {
            CharacterUpdate();
            currentState.OnExecute(this);
        }
    }

    private void RandomDestination(Vector3 startPosition)
    {
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += startPosition;
        NavMeshHit hit;
        
        if (NavMesh.SamplePosition(dir, out hit, radius, 1))
        {
            destinationPos = hit.position;
        }
    }

    public void Moving()
    {
        ChangeAnim(Constant.ANIM_IS_RUN);
        if (Vector3.Distance(destinationPos, tf.position) <= 1f)
        {
            RandomDestination(tf.position);
        }

        agent.SetDestination(destinationPos);
    }

    public void StopMoving()
    {
        ChangeAnim(Constant.ANIM_IS_IDLE);
        agent.SetDestination(tf.position);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public IEnumerator DelayAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Attack();
    }

    public void EnemyAttack(float delayTime)
    {
        StartCoroutine(DelayAttack(delayTime));
    }
}
