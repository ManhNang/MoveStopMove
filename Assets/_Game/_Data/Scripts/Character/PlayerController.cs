using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Character
{
    [SerializeField] private Transform selectEnemy;

    [SerializeField] private bool isAttack;
    [SerializeField] private bool isWin;

    public int Coin => level * 10;

    void Start()
    {
        this.OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        isWin = false;
    }

    void Update()
    {
        if (isDead)
        {
            //joystick.gameObject.SetActive(false);
            agent.velocity = Vector3.zero;
            return;
        }

        CharacterUpdate();
        Move();
        SelectNearEnemy();
        PlayerState();
    }

    private void Move()
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero && GameManager.Instance.IsState(GameState.GamePlay))
        {
            agent.velocity = JoystickControl.direct * moveSpeed;
        }

        if (Input.GetMouseButtonUp(0))
        {
            agent.velocity = Vector3.zero;
        }
    }

    private void SelectNearEnemy()
    {
        SpriteRenderer imgSelectEnemy = selectEnemy.GetComponent<SpriteRenderer>();

        if (listCharacterInRange.Count > 0)
        {
            imgSelectEnemy.enabled = true;

            if (CheckNearestEnemy() != null)
            {
                selectEnemy.position = CheckNearestEnemy().TF.position;
            }
        }

        else
        {
            imgSelectEnemy.enabled = false;
        }
    }

    private void PlayerState()
    {
        if (agent.velocity != Vector3.zero)
        {
            ChangeAnim(Constant.ANIM_IS_RUN);
            isAttack = false;
        }
        else if (!isWin)
        {
            if (IsTargetInRange() && !isAttack)
            {
                ActiveAttack();
            }
            else
            {
                ChangeAnim(Constant.ANIM_IS_IDLE);
            }
        }
    }

    private void ActiveAttack()
    {
        Attack();
        isAttack = true;
    }

    private void StopMoving()
    {
        agent.velocity = Vector3.zero;
    }

    internal void OnRevive()
    {
        ChangeAnim(Constant.ANIM_IS_IDLE);
        isDead = false;
        ClearTarget();
    }

    public void IsWinning()
    {
        isWin = true;
        StopMoving();
        ChangeAnim(Constant.ANIM_IS_DANCE);
    }
}
