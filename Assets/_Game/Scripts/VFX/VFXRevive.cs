using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXRevive : GameUnit
{
    CounterTime counter = new CounterTime();

    private void Update()
    {
        counter.Execute();
    }

    public void OnInit(Vector3 spawnPos, List<Bot> bots)
    {
        counter.Start(() => 
        {
            SpawnBot(spawnPos, bots);
            Invoke(nameof(OnDespawn), 1f);
        }, 1f);
    }

    public void SpawnBot(Vector3 spawnPos, List<Bot> bots)
    {
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPos, Quaternion.identity);
        bot.OnInit();
        LevelManager.Ins.SpawnIndicator(bot);
        bots.Add(bot);
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
