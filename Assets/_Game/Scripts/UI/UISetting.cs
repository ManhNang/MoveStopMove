using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Setting);
    }

    public void ButtonHome()
    {
        LevelManager.Ins.BackToMenu();
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }

    public void ButtonContinue()
    {
        UIManager.Ins.OpenUI<UIGamePlay>();
        Close(0);
    }
}
