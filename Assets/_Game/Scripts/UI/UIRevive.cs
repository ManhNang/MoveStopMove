using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRevive : UICanvas
{
    [SerializeField] TextMeshProUGUI textRevive;
    float counterTimeRevive;

    private void Update()
    {
        if (counterTimeRevive > 0)
        {
            counterTimeRevive -= Time.deltaTime;
            textRevive.SetText(Mathf.Floor(counterTimeRevive).ToString());

            if (counterTimeRevive <= 0)
            {
                Close(0);
                LevelManager.Ins.Fail();
            }
        }
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Revive);
        counterTimeRevive = 5;
    }

    public void ButtonRevive()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Close(0);
        LevelManager.Ins.Revive();
        UIManager.Ins.OpenUI<UIGamePlay>();
    }
    public void ButtonCloseUI()
    {
        LevelManager.Ins.BackToMenu();
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }
}
