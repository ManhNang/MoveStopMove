using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;

    IState<Bot> currentState;

    float radiusCanRun = 20f;
    public bool IsDestination => (Mathf.Abs(destination.x - TF.position.x) + Mathf.Abs(destination.z - TF.position.z)) < 0.5f;
    private bool IsCanRunning => (GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting));

    private void Update()
    {
        if (currentState != null && !isDead && IsCanRunning)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        destination = TF.position;
        level = Random.Range(0, 10);
        UpSize();

        ChangeState(new IdleState());
    }

    public override void WearClothes()
    {
        base.WearClothes();

        //change random 
        ChangeSkin(SkinType.SKIN_Normal);
        ChangeWeapon(Utilities.RandomEnumValue<WeaponType>());
        ChangeHat(Utilities.RandomEnumValue<HatType>());
        ChangeAccessory(Utilities.RandomEnumValue<AccessoryType>());
        ChangePant(Utilities.RandomEnumValue<PantType>());
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
    }

    public override void OnDeath()
    {
        this.StopMoving();
        ChangeState(null);
        base.OnDeath();
        if (GameManager.Ins.gameState == GameState.GamePlay)
        {
            Invoke(nameof(this.OnDespawn), 2f);
        }
    }

    public override void Moving()
    {
        Vector3 destinationRand = RandomDestination();
        SetDestination(destinationRand);
        //LookAtDirection(destination - TF.position);
        base.Moving();
    }

    public override void StopMoving()
    {
        agent.enabled = false;
        destination = TF.position;
        base.StopMoving();
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        agent.SetDestination(destination);
    }

    private Vector3 RandomDestination()
    {
        Vector3 destinationPos = TF.position;

        Vector3 dir = Random.insideUnitSphere * radiusCanRun;
        dir += TF.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(dir, out hit, float.PositiveInfinity, 1))
        {
            destinationPos = hit.position;
            destinationPos.y = TF.position.y;
        }

        if (Vector3.Distance(destinationPos, TF.position) <= 5f)
        {
            destinationPos = RandomDestination();
        }

        return destinationPos;
    }

    public T RandomIndexEnum<T>()
    {
        var enumTypes = System.Enum.GetValues(typeof(T));
        return (T)enumTypes.GetValue(Random.Range(0, enumTypes.Length));
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void RandomStateAfterAttack()
    {
        if (Random.Range(0, 2) == 0)
        {
            ChangeState(new IdleState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
    }
}
