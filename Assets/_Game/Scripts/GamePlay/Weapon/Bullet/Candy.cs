using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Candy : Bullet
{
    protected CounterTime counterCandy = new CounterTime();

    private void Update()
    {
        if (isRunning)
        {
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
            child.Rotate(Vector3.up * -6, Space.Self);
        }

        counterCandy.Execute();
        counter.Execute();
    }

    public override void OnInit(Character throwCharacter, Character victim, UnityAction<Character, Character, Vector3> onHit)
    {
        base.OnInit(throwCharacter, victim, onHit);
        moveSpeed = 7f;
        counterCandy.Start(() => SimplePool.Despawn(this), throwCharacter.Size * 0.8f);
    }
    public override void OnStop()
    {
        counterCandy.Cancel();
        base.OnStop();
    }
}
