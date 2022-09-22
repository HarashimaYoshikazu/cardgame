using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardSkill : ISkill
{
    [SerializeField, SerializeReference, SubclassSelector]
    IAbility _ability = null;
    public IAbility GetAbility => _ability;

    [SerializeField] TargetType _targetType = TargetType.None;
    public TargetType TargetType => _targetType;
}
