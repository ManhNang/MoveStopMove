using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Level currentLevel;
    [SerializeField] public Player player;
    [SerializeField] private List<Bot> bots = new List<Bot>();
    [SerializeField] private int botsDeactive;
    [SerializeField] private int botsActive;

    [SerializeField] Transform canvasGame;
    [SerializeField] TextMeshProUGUI textAlive;

    bool canRevive;
    public int CharacterAmount => botsDeactive + botsActive + 1;
    public bool IsPlaying => GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Setting);

    private void Start()
    {
        currentLevel = levels[0];
        OnInit();
    }

    public void OnInit()
    {
        canRevive = true;
        botsActive = 2;
        botsDeactive = 0;
        
        player = SimplePool.Spawn<Player>(PoolType.Player, currentLevel.StartPoint.position, Quaternion.Euler(0, -180, 0));
        player.OnInit();
        player.SetActiveAttckRange(false);

        CameraFollower.Ins.SetPlayer(player);
        SoundManager.Ins.SetPlayer(player);

        for (int i = 0; i < botsActive; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, SpawnPos(), Quaternion.identity);
            bot.OnInit();
            bots.Add(bot);
            Debug.Log("ss");
        }
    }

    public void NewBot()
    {
        Vector3 spawnPos = SpawnPos();
        VFXRevive vFXRevive = SimplePool.Spawn<VFXRevive>(PoolType.VFX_Revive, spawnPos, Quaternion.identity);
        vFXRevive.OnInit(spawnPos, bots);
    }

    public void CharecterDeath(Character character)
    {
        if (character is Player)
        {
            if (canRevive && IsPlaying)
            {
                UIManager.Ins.CloseAll();
                canRevive = false;
                UIManager.Ins.OpenUI<UIRevive>();
            }
            else
            {
                Fail();
            }
        }

        if (character is Bot)
        {
            bots.Remove(character as Bot);
            botsActive--;
            if (botsDeactive > 0)
            {
                botsDeactive--;
                Invoke(nameof(NewBot), 2.5f);
                botsActive++;
            }

            if (CharacterAmount == 1 && !player.IsDead)
            {
                Victory(player);
            }

            if (CharacterAmount == 0 && player.IsDead)
            {
                Victory(character);
            }
        }

        if (IsPlaying)
        {
            UIManager.Ins.GetUI<UIGamePlay>().SetTextAlive();
        }
    }

    public Vector3 SpawnPos()
    {
        Vector3 spawnPos = Vector3.zero;

        for (int i = 0; i < 50; i++)
        {

            spawnPos = currentLevel.GetSpawnPoint(); 

            for (int j = 0; j < bots.Count; j++)
            {
                if (Vector3.Distance(spawnPos, bots[j].TF.position) < 3f)
                {
                    break;
                }

                if (j == bots.Count - 1)
                {
                    return spawnPos;
                }
            }
        }

        return spawnPos;
    }

    public void Victory(Character character)
    {
        character.Victory();
    }

    public void Fail()
    {
        UIManager.Ins.CloseAll();
        player.SetActiveAttckRange(false);
        UIManager.Ins.OpenUI<UIFail>();
        SoundManager.Ins.PlayOneShot(SoundType.Lose, player); 
    }

    public void Revive()
    {
        ParticlePool.Play(ParticleType.Revive, player.TF.position);
        player.Revive();
    }

    List<string> nameBots = new List<string>()
    {
        "Chovy", "Faker", "Optimus", "Levi", "Sofm", "Tarzan", "Bin", "Zeus", "Zeros", "Slayder", "Gumayusi", "Keria", "Kingen", "Lucid", "Showmaker",
        "Aiming", "Oner", "Kiin", "Canyon", "Peyz", "Lehends", "Mata", "Rascal", "Teddy", "Clozer", "Morgan", "Effort", "Pyosik", "Bdd", "Deft", "BeryL",
        "Doran", "Peanut", "Zeka", "Viper", "Delight", "Hope", "Knight", "Elk", "LvMao", "Ale", "Fisher", "Missing", "Ruler", "Flandre", "Kavani", "Yagao",
        "Scout", "Gala", "Weiwei", "Rookie", "Angel", "Ming", "Wayward", "Tian", "JackeyLove", "Meiko", "Doggo", "Crisp", "Light", "Xiaohu", "Xiaohao"
    };

    public void SpawnIndicator(Character character)
    {
        if (!character.IsIndicator)
        {
            TargetIndicator indicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
            indicator.OnInit(character);
            character.SetIndicator(indicator);
            indicator.TF.SetParent(canvasGame);
            indicator.TF.localScale = canvasGame.localScale;

            if (character is Player)
            {
                indicator.SetName("Player");
            }

            if (character is Bot)
            {
                int index = Random.Range(0, nameBots.Count);
                indicator.SetName(nameBots[index]);
                nameBots.RemoveAt(index);
            }
        }
    }

    public void OnPlayGame()
    {
        SpawnIndicator(player);

        for (int i = 0; i < bots.Count; i++)
        {
            SpawnIndicator(bots[i]);
        }

        player.SetActiveAttckRange(true);
    }

    public void BackToMenu()
    {
        OnDespawn();
        OnInit();
        UIManager.Ins.CloseAll();
    }

    public void OnDespawn()
    {
        player.OnDespawn();

        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].OnDespawn();
        }

        bots.Clear();
        SimplePool.CollectAll();
    }
}
