using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRevive : UICanvas
{
    [SerializeField] Text counterTxt;
    private float counter;

    public override void Setup()
    {
        base.Setup();
        GameManager.Instance.ChangeState(GameState.Revive);
        counter = 5;
    }

    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            counterTxt.text = counter.ToString("F0");

            if (counter <= 0)
            {
                CloseButton();
            }
        }
    }

    public void ReviveButton()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        Close(0);
        LevelManager.Instance.OnRevive();
        UIManager.Instance.OpenUI<UIGamePlay>();
    }

    public void CloseButton()
    {
        Close(0);
        LevelManager.Instance.Fail();
    }
}
