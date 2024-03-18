using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : GameUnit
{
    [SerializeField] protected Character throwCharacter; 
    [SerializeField] protected Transform child;
    protected CounterTime counter = new CounterTime();
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool isRunning = false;

    protected UnityAction<Character, Character, Vector3> onHit;

    Vector3 posOnHit => TF.position + Vector3.up * 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER) && isRunning)
        {
            Character victim = Cache<Character>.GetT(other);

            if (victim != null && victim != throwCharacter && !victim.IsDead)
            {
                throwCharacter.RemoveVictim(victim);
                SimplePool.Despawn(this);
                victim.OnDeath();
                onHit?.Invoke(throwCharacter, victim, posOnHit);
            }
        }

        if (other.CompareTag(Constant.TAG_BLOCK))
        {
            OnStop();
        }
    }

    public virtual void OnInit(Character throwCharacter, Character victim, UnityAction<Character, Character, Vector3> onHit)
    {
        this.throwCharacter = throwCharacter;
        throwCharacter.SetSize(this.TF);
        this.onHit = onHit;
        isRunning = true;
        Vector3 direction = (victim.TF.position - throwCharacter.TF.position).normalized;
        TF.forward = (direction != Vector3.zero) ? direction : throwCharacter.TF.forward;
    }

    public virtual void OnStop()
    {
        isRunning = false;
        counter.Start(() => SimplePool.Despawn(this), 1f);
    }
}
