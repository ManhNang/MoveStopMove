using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Throw = 0,
    OnHit = 1,
    SizeUp = 2,
    Die = 3,
    Lose = 4,
    Victory = 5,
}

public class SoundManager : Singleton<SoundManager>
{

    Player player;

    [SerializeField] AudioSource[] soundAudioSources;

    public void SetVolume(int volume)
    {
        for (int i = 0; i < soundAudioSources.Length; i++)
        {
            soundAudioSources[i].volume = volume;
            soundAudioSources[i].mute = volume == 0;
        }
    }

    public void PlayOneShot(SoundType soundType, Character character)
    {
        float distance = Vector3.Distance(player.TF.position, character.TF.position);
        int index = (int)soundType;
        if ( index < soundAudioSources.Length && distance < 10f)
        {
            if (distance > 1f)
            {
                soundAudioSources[index].volume = 1f / distance;
            }

            soundAudioSources[index].PlayOneShot(soundAudioSources[index].clip);
        }
    }

    public void PlayMusic(SoundType soundType)
    {
        int index = (int)soundType;
        if (index < soundAudioSources.Length)
        {
            soundAudioSources[index].Play();
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
