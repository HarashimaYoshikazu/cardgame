using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbility : IAbility
{
    [SerializeField]
    int _damage = 0;
    public AbilityType AbilityType => AbilityType.Damage;

    public void Execute()
    {
        Debug.Log("ダメージアビリティ");
    }
}
