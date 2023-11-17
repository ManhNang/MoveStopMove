using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFail : UICanvas
{
    private int coin;
    [SerializeField] private Text coinTxt;

    public override void Open()
    {
        base.Open();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void MainMenuButton()
    {
        LevelManager.Instance.Home();
    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.text = coin.ToString();
    }
}
