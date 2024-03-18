using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Hammer : Bullet
{
    protected CounterTime counterHammer = new CounterTime();

    private void Update()
    {
        if (isRunning)
        {
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
            child.Rotate(Vector3.forward * -8, Space.Self);
        }

        counterHammer.Execute();
        counter.Execute();
    }

    public override void OnInit(Character throwCharacter, Character victim, UnityAction<Character, Character, Vector3> onHit)
    {
        base.OnInit(throwCharacter, victim, onHit);
        moveSpeed = 6f;
        counterHammer.Start(() => SimplePool.Despawn(this), throwCharacter.Size * 0.8f);
    }

    public override void OnStop()
    {
        counterHammer.Cancel();
        base.OnStop();
    }
}
