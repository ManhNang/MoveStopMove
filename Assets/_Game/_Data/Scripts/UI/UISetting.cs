using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.Instance.ChangeState(GameState.Setting);
        UIManager.Instance.CloseUI<UIGamePlay>();
    }

    public void HomeButton()
    {
        LevelManager.Instance.Home();
    }

    public void ContinueButton()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        UIManager.Instance.OpenUI<UIGamePlay>();
        Close(0);
    }
}
