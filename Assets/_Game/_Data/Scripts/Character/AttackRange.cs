using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : GameUnit
{
    [SerializeField] private Character myCharacter;

    private float radius = 5f;
    public float Radius => radius;

    void Start()
    {
        tf = this.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            Character enemyChar = other.GetComponent<Character>();

            if (other.CompareTag(Constant.TAG_LAYER_CHAR) && other != myCharacter && !enemyChar.isDead)
            {
                myCharacter.AddEnemy(enemyChar);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            Character enemyChar = other.GetComponent<Character>();

            if (other.CompareTag(Constant.TAG_LAYER_CHAR) && other != myCharacter)
            {
                myCharacter.RemoveEnemy(enemyChar);
            }
        }
    }
    public void OnInit()
    {
        tf.localScale = Vector3.one * radius;
    }
}
