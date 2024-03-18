using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFail : UICanvas
{
    [SerializeField] TextMeshProUGUI textRank;
    [SerializeField] TextMeshProUGUI textNameKiller;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
        SetRank(); 
        SetKiller();
    }

    public void ButtonContinue()
    {
        LevelManager.Ins.BackToMenu();
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }

    public void SetRank()
    {
        textRank.SetText("#" + UIManager.Ins.GetUI<UIGamePlay>().Rank.ToString());
    }

    public void SetKiller()
    {
        textNameKiller.SetText(LevelManager.Ins.player.Killer);
    }
}
