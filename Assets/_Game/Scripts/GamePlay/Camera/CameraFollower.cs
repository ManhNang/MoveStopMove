using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : Singleton<CameraFollower>
{
    public enum CameraState { MainMenu, GamePlay, Shop}

    public Transform TF;
    Player player;

    [SerializeField] Transform[] offsets;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private Quaternion targetRotate;

    [SerializeField] float moveSpeed = 5f;

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        if (LevelManager.Ins.IsPlaying)
        {
            player.GetPinVictim.SetActivePin(player);
        }

        TF.position = Vector3.Lerp(TF.position, player.TF.position + targetOffset, Time.deltaTime * moveSpeed);
        TF.rotation = Quaternion.Lerp(TF.rotation, targetRotate, Time.deltaTime * moveSpeed);
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetRateOffset()
    {
        targetOffset += targetOffset * 0.03f;
    }

    public void ChangeState(CameraState state)
    {
        targetOffset = offsets[(int)state].localPosition;
        targetRotate = offsets[(int)state].localRotation;
    }
}
