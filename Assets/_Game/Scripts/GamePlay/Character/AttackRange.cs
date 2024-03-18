using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] GameObject child;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character victim = Cache<Character>.GetT(other);
            if (victim != null && !victim.IsDead)
            {
                character.AddVictim(victim);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character victim = Cache<Character>.GetT(other);
            if (victim != null)
            {
                character.RemoveVictim(victim);
            }
        }
    }

    public void SetActive(bool active)
    {
        child.SetActive(active);
    }
}
