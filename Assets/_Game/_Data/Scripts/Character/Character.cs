using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class Character : GameUnit
{
    [Header("Tranform")]
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator anim;
    [SerializeField] protected AttackRange attackRange;
    [SerializeField] protected SpawnWeapon attackSpawn;

    [Header("Data")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int level;
    public int Level => level;

    [SerializeField] protected float size;
    public float Size => size;

    [SerializeField] protected float radiusAttack;
    public float RadiusAttack => radiusAttack;

    [SerializeField] protected List<Character> listCharacterInRange = new List<Character>();

    public List<Vector3> currentAttackPos;

    protected string currentAnim = Constant.ANIM_IS_IDLE;

    public bool isDead = false;

    public virtual void OnInit()
    {
        ChangeAnim(Constant.ANIM_IS_IDLE);

        isDead = false;
        moveSpeed = 20f;
        level = 0;
        size = 1f;

        ClearTarget();

        attackRange.OnInit();
        attackSpawn.OnInit();
    }

    protected void CharacterUpdate()
    {
        if (isDead)
        {
            return;
        }

        radiusAttack = attackRange.TF.lossyScale.x;

        DespawnWeaponBullet();
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.SetBool(animName, true);

            anim.SetBool(currentAnim, false);

            currentAnim = animName;
        }
    }

    public virtual void Attack()
    {
        if (listCharacterInRange.Count > 0 && !isDead && CheckNearestEnemy() != null)
        {
            tf.LookAt(CheckNearestEnemy().transform);

            ChangeAnim(Constant.ANIM_IS_ATTACK);
            attackSpawn.SpawnWeaponFromPool();
            currentAttackPos.Add(attackSpawn.TF.position);
        }
    }

    public bool IsTargetInRange()
    {
        if (listCharacterInRange.Count > 0)
        {
            return true;
        }

        return false;
    }

    public Character CheckNearestEnemy()
    {
        Character nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Character enemy in listCharacterInRange)
        {
            if (enemy.isDead)
            {
                listCharacterInRange.Remove(enemy);
                break;
            }
        }

        if (listCharacterInRange.Count > 0)
        {
            foreach (Character enemy in listCharacterInRange)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.TF.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }
        }

        return nearestEnemy;
    }

    public void AddEnemy(Character enemy)
    {
        listCharacterInRange.Add(enemy);
    }

    public void RemoveEnemy(Character enemy)
    {
        listCharacterInRange.Remove(enemy);
    }

    protected void DespawnWeaponBullet()
    {
        if (currentAttackPos.Count > 0)
        {
            for (int i = 0; i < currentAttackPos.Count; i++)
            {
                Vector3 bulletPos = attackSpawn.BulletUsing[i].transform.position;
                Weapon weapon = attackSpawn.BulletUsing[i].transform.GetComponent<Weapon>();

                if (Vector3.Distance(bulletPos, currentAttackPos[i]) >= radiusAttack + 1)
                {
                    OnDespawnBullet(i, weapon);
                }

                if (weapon.isOnHit && weapon.characterOnHit != null)
                {
                    Character charOnHit = weapon.characterOnHit.GetComponent<Character>();
                    OnDespawnBullet(i, weapon);

                    LevelUp(weapon.characterOnHit);
                    listCharacterInRange.Remove(charOnHit);
                    charOnHit.OnHit();
                }
            }
        }
    }

    protected void LevelUp(Transform characterOnHit)
    {
        int levelCharacter = characterOnHit.GetComponent<Character>().Level;
        if (levelCharacter < 2)
        {
            this.level += 1;
        }
        else if (levelCharacter < 6)
        {
            this.level += 2;
        }
        else if (levelCharacter < 10)
        {
            this.level += 3;
        }
        else
        {
            this.level += 4;
        }

        SizeUp();
    }

    private void SizeUp()
    {
        this.size += this.level * 0.03f;
        tf.localScale = this.size * new Vector3(3, 3, 3);
    }

    protected void OnHit()
    {
        OnDeath();
    }

    private void OnDeath()
    {
        ChangeAnim(Constant.ANIM_IS_DEAD);
        isDead = true;

        foreach (GameObject bullet in attackSpawn.BulletUsing)
        {
            PoolingWeapon.Instance.ResetWeapon(bullet);
        }

        Invoke(nameof(OnDespawn), 2f);

    }

    private void OnDespawn()
    {
        LevelManager.Instance.CharecterDeath(this);
    }

    private void OnDespawnBullet(int index, Weapon weapon)
    {
        PoolingWeapon.Instance.ResetWeapon(attackSpawn.BulletUsing[index]);

        weapon.isOnHit = false;

        attackSpawn.BulletUsing.RemoveAt(index);
        currentAttackPos.RemoveAt(index);
    }

    public void ClearTarget()
    {
        listCharacterInRange.Clear();
    }
}
