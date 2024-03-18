using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] private TextMeshProUGUI coinTxt;
    [SerializeField] private RectTransform x3Point;
    [SerializeField] private RectTransform mainMenuPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void ButtonNextZone()
    {
        LevelManager.Ins.OnDespawn();
        LevelManager.Ins.OnInit();
        UIManager.Ins.OpenUI<UIGamePlay>();
        Close(0);
    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.SetText(coin.ToString());
    }
}
