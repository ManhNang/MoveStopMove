using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] Text coinTxt;

    public override void Open()
    {
        base.Open();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void HomeButton()
    {
        LevelManager.Instance.Home();
    }

    public void NextAreaButton()
    {
        LevelManager.Instance.NextLevel();
        LevelManager.Instance.Home();
    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.text = coin.ToString();
    }
}
