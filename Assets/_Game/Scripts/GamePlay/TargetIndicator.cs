using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{
    [SerializeField] Image iconLevel;
    [SerializeField] Image iconDirect;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] RectTransform rect;
    [SerializeField] RectTransform direct;

    public string NameTarget => nameText.text;

    public Character character;
    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2 (Screen.width, Screen.height) / 2;

    Vector2 viewPointInCameraX = new Vector2(0.02f, 0.98f);
    Vector2 viewPointInCameraY = new Vector2(0.005f, 0.995f);

    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void LateUpdate()
    {
        viewPoint = Camera.main.WorldToViewportPoint(character.PosIndicatorPoint);

        direct.gameObject.SetActive(!IsInCamera);
        nameText.gameObject.SetActive(IsInCamera);

        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointInCameraX.x, viewPointInCameraX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointInCameraY.x, viewPointInCameraY.y);

        Vector3 targetSPoint = Camera.main.ViewportToScreenPoint(viewPoint) - screenHalf;
        Vector3 playerSPoint = Camera.main.WorldToScreenPoint(LevelManager.Ins.player.TF.position) - screenHalf;

        rect.anchoredPosition = targetSPoint / (Screen.width / 1000f);

        direct.up = (targetSPoint - playerSPoint).normalized;
    }

    public void OnInit(Character character)
    {
        this.character = character;
        Color color = new Color(Random.value, Random.value, Random.value, 255);
        SetColor(color);
        SetLevel();
    }

    public void SetLevel()
    {
        levelText.SetText(character.Level.ToString());
    }

    public void SetColor(Color color)
    {

        iconLevel.color = color;
        iconDirect.color = color;
        nameText.color = color;
    }

    public void SetName(string name)
    {
        nameText.SetText(name);
    }
}
