using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollower.Ins.ChangeState(CameraFollower.CameraState.MainMenu);
        coinText.SetText("999");
    }

    public void PlayButton()
    {
        UIManager.Ins.OpenUI<UIGamePlay>();
        CameraFollower.Ins.ChangeState(CameraFollower.CameraState.GamePlay);
        Close(0);
    }

    public void SkinShopButton()
    {
        UIManager.Ins.OpenUI<UISkinShop>();
        Close(0);
    }


    public void WeaponShopButton()
    {
        UIManager.Ins.OpenUI<UIWeaponShop>();
        Close(0);
    }
}
