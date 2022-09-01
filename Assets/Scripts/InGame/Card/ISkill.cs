using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    TargetType TargetType { get; }
}
public enum TargetType
{
    None,
    RandomOne,
    SelectOne,
    SelectFriend,
    SelectOpponent,
    AllFriends,
    AllOpponents,
    All,
}
