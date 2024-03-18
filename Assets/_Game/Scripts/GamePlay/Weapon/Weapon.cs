using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.TextCore.Text;
using UnityEngine.Events;

public class Weapon : GameUnit
{
    CounterTime counter = new CounterTime();
    [SerializeField] BulletType bulletType;
    [SerializeField] GameObject child;

    public bool IsCanAttack => child.activeSelf;

    private void Update()
    {
        counter.Execute();
    }

    public void Throw(Character attacker, Character victim, Vector3 spawnPoint, UnityAction<Character, Character, Vector3> onHit)
    {
        if (victim != null && !victim.IsDead && !attacker.IsDead)
        {
            SetActive(false);
            SoundManager.Ins.PlayOneShot(SoundType.Throw, attacker);
            Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, spawnPoint, Quaternion.identity);
            bullet.OnInit(attacker, victim, onHit);
            counter.Start(() => SetActive(true), 0.5f);
        }
    }

    public void SetActive(bool active)
    {
        child.SetActive(active);
    }
}
