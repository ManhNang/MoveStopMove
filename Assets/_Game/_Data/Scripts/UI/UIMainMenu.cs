using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    //[SerializeField] TextMeshProUGUI playerCoinTxt;
    //[SerializeField] RectTransform coinPoint;
    public override void Open()
    {
        base.Open();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public void ShopButton()
    {
        //UIManager.Instance.OpenUI<UIShop>();
        Close(0);
    }


    public void WeaponButton()
    {
        //UIManager.Instance.OpenUI<UIWeapon>();
        Close(0);
    }

    public void PlayButton()
    {
        LevelManager.Instance.currentLevel.OnPlay();
        UIManager.Instance.OpenUI<UIGamePlay>();

        Close(0.5f);
    }
}
