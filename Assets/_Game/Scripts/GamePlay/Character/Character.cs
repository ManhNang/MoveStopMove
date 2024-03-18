using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character : AbsCharacter
{
    [SerializeField] protected Skin currentSkin;

    [SerializeField] protected Transform body;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Transform indicatorPoint;
    public Vector3 PosAttackPoint => attackPoint.position;
    public Vector3 PosIndicatorPoint => indicatorPoint.position;

    [SerializeField] protected List<Character> victims = new List<Character>();
    [SerializeField] protected Character victimNearest;
    public Character VictimNearest => victimNearest;
    private CounterTime counterTime = new CounterTime();
    public CounterTime Counter => counterTime;


    protected float moveSpeed = 5f;
    [SerializeField] protected float size;
    [SerializeField] protected int level;
    [SerializeField] protected bool isDead = false;
    private string currentAnim;
    protected string nameKiller;

    public Skin CurrentSkin => currentSkin;

    TargetIndicator indicator;
    public float Size => size;
    public int Level => level;
    public int CountVictims => victims.Count;
    public bool IsDead => isDead;
    public bool IsIndicator => indicator != null;
    public string Killer => nameKiller;
    public bool CanAttack()
    {
        if (currentSkin.CurrentWeapon == null) return false;
        else return currentSkin.CurrentWeapon.IsCanAttack;
    }
    public override void OnInit()
    {
        isDead = false;
        WearClothes();
        ClearVictims();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public virtual void WearClothes()
    {

    }

    public virtual void TakeOffClothes()
    {
        currentSkin?.OnDespawn();
        SimplePool.Despawn(currentSkin);
    }

    public override void OnAttack()
    {
        for (int i = 0; i < victims.Count; i++)
        {
            if (victims[i].IsDead)
            {
                victims.Remove(victims[i]);
            }
        }

        victimNearest = DectectVictimNearest();

        if (victimNearest != null && !victimNearest.IsDead && CanAttack() && !IsDead)
        {
            Vector3 direction = victimNearest.TF.position - TF.position;
            direction.y = 0f;
            TF.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Constant.ANIM_ATTACK);
        }
    }

    public override void OnDespawn()
    {
        currentSkin.OnDespawn();
        SimplePool.Despawn(indicator);
        indicator = null;
    }

    public override void OnDeath()
    {
        isDead = true;
        LevelManager.Ins.CharecterDeath(this); 
        SoundManager.Ins.PlayOneShot(SoundType.Die, this);
        ChangeAnim(Constant.ANIM_DEAD);
    }

    public virtual void Moving()
    {
        ChangeAnim(Constant.ANIM_RUN);
    }

    public virtual void StopMoving()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public virtual void Victory()
    {
        this.StopMoving();
        ChangeAnim(Constant.ANIM_DANCE);
    }

    public virtual void AddVictim(Character victim)
    {
        victims.Add(victim);
    }

    public virtual void RemoveVictim(Character victim)
    {
        victims.Remove(victim);
    }

    public void LookAtDirection(Vector3 direction)
    {
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            TF.rotation = Quaternion.LookRotation(direction);
        }
    }

    public Character DectectVictimNearest()
    {
        Character victim = null;
        float nearestDistance = Mathf.Infinity;

        for (int i = 0; i < victims.Count; i++)
        {
            if (victims[i] != null && victims[i] != this && !victims[i].IsDead)
            {
                float distance = Vector3.Distance(TF.position, victims[i].TF.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    victim = victims[i];
                }
            }
        }

        return victim;
    }
    public virtual void OnHitVictim(Character throwCharacter, Character victim, Vector3 posOnHit)
    {
        ParticlePool.Play(ParticleType.OnHit, posOnHit);
        SoundManager.Ins.PlayOneShot(SoundType.OnHit, victim);
        throwCharacter.UpLevel(victim);

        victim.SetKiller(throwCharacter.indicator.NameTarget);
    }

    public virtual void UpSize()
    {
        size = 1 + (level * 0.03f);
        SetSize(this.TF);
    }

    public void SetSize(Transform obj)
    {
        obj.localScale = size * Vector3.one;
    }

    public void UpLevel(Character victim)
    {
        level = (victim.Level <= 3) ? level += 1 : level += Mathf.FloorToInt(Mathf.Sqrt(victim.Level));
        ParticlePool.Play(ParticleType.LevelUp, TF.position + Vector3.up);
        indicator.SetLevel();
        UpSize();
        SoundManager.Ins.PlayOneShot(SoundType.SizeUp, this);
    }

    public void ChangeAnim(string animName)
    {
        if (this.currentAnim != animName)
        {
            currentSkin.Anim.ResetTrigger(this.currentAnim);
            this.currentAnim = animName;
            currentSkin.Anim.SetTrigger(this.currentAnim);
        }
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        if (currentSkin.CurrentWeapon != null)
        {
            currentSkin.OnDespawn();
        }

        currentSkin.ChangeWeapon(weaponType);
    }

    public void ChangeSkin(SkinType skinType)
    {
        currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        currentSkin.ChangeAccessory(accessoryType);
    }

    public void ChangeHat(HatType hatType)
    {
        currentSkin.ChangeHat(hatType);
    }

    public void ChangePant(PantType pantType)
    {
        currentSkin.ChangePant(pantType);
    }

    public void ResetAnim()
    {
        this.currentAnim = "";
    }

    public void SetIndicator(TargetIndicator indicator)
    {
        this.indicator = indicator;
    }

    public void SetKiller(string nameKiller)
    {
        this.nameKiller = nameKiller;
    }

    public void ClearVictims()
    {
        victims.Clear();
        victimNearest = null;
    }
}
