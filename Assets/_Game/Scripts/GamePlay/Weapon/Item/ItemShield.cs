using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    [SerializeField] VFXShield vfxShieldPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character character = Cache<Character>.GetT(other);
            if (character != null && !character.IsDead)
            {
                VFXShield vfxShield = SimplePool.Spawn<VFXShield>(vfxShieldPrefab, character.TF);
                vfxShield.Shield.Play();
            }
        }
    }
}
