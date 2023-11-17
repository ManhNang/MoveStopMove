using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public Text characterAmountTxt;

    public override void Setup()
    {
        base.Setup();
        UpdateTotalCharacter();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }

    public void UpdateTotalCharacter()
    {
        characterAmountTxt.text = LevelManager.Instance.TotalCharater.ToString();
    }
}
