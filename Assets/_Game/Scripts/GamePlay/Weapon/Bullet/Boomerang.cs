using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Boomerang : Bullet
{
    Vector3 target;
    bool arrivedDestination;

    private void Update()
    {
        if (isRunning)
        {
            if (!arrivedDestination)
            {
                TF.position = Vector3.MoveTowards(TF.position, target, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(TF.position, target) < 0.1f)
                {
                    arrivedDestination = true;
                }
            }
            else
            {
                TF.position = Vector3.MoveTowards(TF.position, throwCharacter.TF.position, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(TF.position, throwCharacter.TF.position) < 0.1f || throwCharacter.IsDead)
                {
                    SimplePool.Despawn(this);
                }
            }

            child.Rotate(Vector3.forward * 6, Space.Self);
        }

        counter.Execute();
    }
    public override void OnInit(Character throwCharacter, Character victim, UnityAction<Character, Character, Vector3> onHit)
    {
        base.OnInit(throwCharacter, victim, onHit);
        moveSpeed = 5f;
        arrivedDestination = false;
        target = (victim.TF.position - throwCharacter.TF.position).normalized * (Constant.ATTACK_RANGE + 1) * throwCharacter.Size + throwCharacter.TF.position;
    }
}
