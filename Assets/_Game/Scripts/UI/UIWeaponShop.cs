using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponShop : UICanvas
{
    [SerializeField] Transform weaponParent;
    [SerializeField] Button buttonEquiped;
    [SerializeField] Button buttonSelect;

    int weaponIndex;

    public override void Open()
    {
        base.Open();
        weaponIndex = 0;
        OnInitWeapon(weaponIndex);
    }

    public void OnInitWeapon(int weaponIndex)
    {
        foreach (Transform child in weaponParent)
        {
            Destroy(child.gameObject);
        }

        Instantiate(GameManager.Ins.WeaponShopData.WeaponsItem[weaponIndex].prefabs, weaponParent.position, Quaternion.identity, weaponParent);

        if (weaponIndex == PlayerPrefs.GetInt("Weapon"))
        {
            buttonEquiped.gameObject.SetActive(true);
            buttonSelect.gameObject.SetActive(false);
        }
        else
        {
            buttonEquiped.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(true);
        }
    }

    public void ButtonPrevious()
    {
        if (weaponIndex > 0)
        {
            weaponIndex--;
            if (weaponIndex < GameManager.Ins.WeaponShopData.weaponsCount)
            {
                OnInitWeapon(weaponIndex);
            }
        }
    }

    public void ButtonNext()
    {
        if (weaponIndex < GameManager.Ins.WeaponShopData.weaponsCount - 1)
        {
            weaponIndex++;
            OnInitWeapon(weaponIndex);
        }
    }

    public void ButtonCloseUI()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }

    public void ButtonSelect()
    {
        PlayerPrefs.SetInt("Weapon", weaponIndex);
        LevelManager.Ins.player.ChangeWeapon(GameManager.Ins.GetCurrentWeapon(weaponIndex));
        buttonEquiped.gameObject.SetActive(true);
        buttonSelect.gameObject.SetActive(false);
    }
}
