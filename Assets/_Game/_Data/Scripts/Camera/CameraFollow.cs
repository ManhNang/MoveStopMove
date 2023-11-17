using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : GameUnit
{

    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float smoothSpeed;

    void Start()
    {
        tf = this.transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            tf.position = Vector3.Lerp(tf.position, desiredPosition, smoothSpeed);
            tf.rotation = Quaternion.LookRotation(player.position);

            tf.LookAt(player);
        }
    }
}
