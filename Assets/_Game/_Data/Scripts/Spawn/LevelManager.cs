using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PlayerController player;

    [SerializeField] private List<Level> levels;
    public Level currentLevel;
    private List<EnemyController> bots;

    private int levelIndex;
    private int totalCharacter;
    public int TotalCharater { get => totalCharacter; }

    private bool isRevive;

    void Start()
    {
        levelIndex = 0;
        OnLoadLevel(levelIndex);
        OnInit();
    }

    private void OnInit()
    {
        currentLevel.SpawnEnemy();
        bots = currentLevel.ListEnemyAlive;
        totalCharacter = bots.Count + 1;

        isRevive = false;
    }

    public void OnReset()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Destroy(bots[i].gameObject);
        }

        bots.Clear();
        player.OnInit();
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    private void Victory()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIVictory>().SetCoin(player.Coin);
        player.ChangeAnim(Constant.ANIM_IS_WIN);
    }

    public void Home()
    {
        UIManager.Instance.CloseAll();
        OnReset();
        OnLoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<UIMainMenu>();
    }

    public void NextLevel()
    {
        levelIndex++;
    }

    public void OnPlay()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());
        }
    }

    public void OnRevive()
    {
        player.OnRevive();
    }

    public void Fail()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIFail>().SetCoin(player.Coin);
    }

    public void EnemyIsDead(EnemyController enemy)
    {
        if (bots.Count > 0 && bots.Contains(enemy))
        {
            currentLevel.RemoveEnemy(enemy);
        }
    }

    public void CharecterDeath(Character c)
    {
        if (c is PlayerController)
        {
            UIManager.Instance.CloseAll();

            //revive
            if (!isRevive)
            {
                isRevive = true;
                UIManager.Instance.OpenUI<UIRevive>();
            }
            else
            {
                Fail();
            }
        }
        else
        if (c is EnemyController)
        {
            currentLevel.RemoveEnemy(c as EnemyController);

            if (totalCharacter > 0)
            {
                totalCharacter--;
            }

            if (bots.Count == 0)
            {
                Victory();
            }

        }

        UIManager.Instance.GetUI<UIGamePlay>().UpdateTotalCharacter();
    }

    
}
