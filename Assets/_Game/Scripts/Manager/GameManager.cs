using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Revive, Setting }

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    [SerializeField] WeaponShopData weaponShopData;

    int weaponIndex;
    public int WeaponIndex => weaponIndex;
    public WeaponShopData WeaponShopData => weaponShopData;

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //Init data
        //UserData.Ins.OnInitData();
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<UIMainMenu>(); 
    }

    public int LoadWeapon()
    {
        if (!PlayerPrefs.HasKey("Weapon"))
        {
            PlayerPrefs.SetInt("Weapon", 0);
        }

        return PlayerPrefs.GetInt("Weapon");
    }

    public WeaponType GetCurrentWeapon(int index)
    {
        return weaponShopData.WeaponsItem[index].weaponType;
    }
}
