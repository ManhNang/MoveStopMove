using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UISkinShop;
using UnityEngine.Rendering;

public class Player : Character
{
    [SerializeField] AttackRange attackRange;
    [SerializeField] PinVictim pinVictim;
    CounterTime counterAttack = new CounterTime();

    private bool isMoving = false;

    public PinVictim GetPinVictim => pinVictim;
    private bool IsCanUpdate => GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Setting);

    SkinType skinType = SkinType.SKIN_Normal;
    WeaponType weaponType = WeaponType.W_Candy_1;
    HatType hatType = HatType.HAT_None;
    AccessoryType accessoryType = AccessoryType.ACC_None;
    PantType pantType = PantType.Pant_1;

    private void Update()
    {
        if (IsCanUpdate && !isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Counter.Cancel();
                counterAttack.Cancel();
            }

            if (Input.GetMouseButton(0))
            {
                this.Moving();
                isMoving = true;
            }
            else
            {
                Counter.Execute();
                counterAttack.Execute();
            }

            if (Input.GetMouseButtonUp(0))
            {
                this.StopMoving();
                isMoving = false;
                this.OnAttack();
            }
        }
    }

    public override void OnInit()
    {
        OnTakeClothsData();

        base.OnInit();
        level = 0;
        UpSize();
    }

    public override void OnAttack()
    {
        base.OnAttack();
        if (victimNearest != null && !victimNearest.IsDead && CanAttack() && !IsDead)
        {
            Counter.Start(() => currentSkin.CurrentWeapon.Throw(this, victimNearest, PosAttackPoint, victimNearest.OnHitVictim), Constant.DELAY_TIME_THROW);
            ResetAnim();
            counterAttack.Start(OnAttack, 2f);
        }
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
        this.StopMoving();
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    public override void Moving()
    {
        if (JoystickControl.direct != Vector3.zero)
        {
            TF.position += JoystickControl.direct * moveSpeed * Time.deltaTime;
            LookAtDirection(JoystickControl.direct);
            base.Moving();
        }
    }

    public override void StopMoving()
    {
        base.StopMoving();
    }

    public override void Victory()
    {
        base.Victory();
        SetActiveAttckRange(false);
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIVictory>();
        SoundManager.Ins.PlayOneShot(SoundType.Victory, this);
    }

    public override void AddVictim(Character victim)
    {
        base.AddVictim(victim);
        if (!isDead && !victim.IsDead && IsCanUpdate)
        {
            if (!Counter.IsRunning && !counterAttack.IsRunning && !isMoving)
            {
                this.OnAttack();
            }
        }
    }

    public override void UpSize()
    {
        base.UpSize();
        CameraFollower.Ins.SetRateOffset();
    }

    public void Revive()
    {
        isDead = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public void SetActiveAttckRange(bool active)
    {
        pinVictim.SetActive(active);
        attackRange.SetActive(active);
    }

    public override void WearClothes()
    {
        base.WearClothes();

        ChangeSkin(skinType);
        ChangeWeapon(weaponType);
        ChangeHat(hatType);
        ChangeAccessory(accessoryType);
        ChangePant(pantType);
    }

    public void TryCloth(UISkinShop.ShopSkinType SkinType, Enum type)
    {
        switch (SkinType)
        {
            case UISkinShop.ShopSkinType.Hat:
                currentSkin.DespawnHat();
                ChangeHat((HatType)type);
                break;

            case UISkinShop.ShopSkinType.Pant:
                ChangePant((PantType)type);
                break;

            case UISkinShop.ShopSkinType.Accessory:
                currentSkin.DespawnAccessory();
                ChangeAccessory((AccessoryType)type);
                break;

            case UISkinShop.ShopSkinType.Skin:
                TakeOffClothes();
                skinType = (SkinType)type;
                WearClothes();
                break;
            case UISkinShop.ShopSkinType.Weapon:
                currentSkin.DespawnWeapon();
                ChangeWeapon((WeaponType)type);
                break;
            default:
                break;
        }

    }

    //take cloth from data
    internal void OnTakeClothsData()
    {
        // take old cloth data
        skinType = UserData.Ins.playerSkin;
        weaponType = UserData.Ins.playerWeapon;
        hatType = UserData.Ins.playerHat;
        accessoryType = UserData.Ins.playerAccessory;
        pantType = UserData.Ins.playerPant;
    }
}
