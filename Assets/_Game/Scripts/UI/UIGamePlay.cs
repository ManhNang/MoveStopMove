using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI textAlive;
    [SerializeField] JoystickControl joystick;
    private int rank;
    public int Rank => rank;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        joystick.gameObject.SetActive(true);
        LevelManager.Ins.OnPlayGame();
        SetTextAlive();
    }

    public void ButtonSetting()
    {
        joystick.gameObject.SetActive(false);
        UIManager.Ins.OpenUI<UISetting>();
        Close(0);
    }

    public void SetTextAlive()
    {
        rank = LevelManager.Ins.CharacterAmount;
        textAlive.SetText("Alive: " + LevelManager.Ins.CharacterAmount.ToString());
    }
}
