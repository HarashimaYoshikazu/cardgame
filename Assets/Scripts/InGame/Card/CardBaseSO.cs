using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class CardBaseSO : ScriptableObject
{
    [SerializeField, Tooltip("カードの名前")]
    string _name;
    public string Name
    {
        get { return _name; }
#if UNITY_EDITOR
        set { _name = value; }
#endif
    }

    [SerializeField, Tooltip("攻撃力")]
    int _attack = 0;
    public int Attack
    {
        get { return _attack; }
#if UNITY_EDITOR
        set { _attack = value; }
#endif
    }

    [SerializeField, Tooltip("最大HP")]
    int _maxHP = 0;
    public int MAXHP
    {
        get { return _maxHP; }
#if UNITY_EDITOR
        set { _maxHP = value; }
#endif
    }

    [SerializeField, Tooltip("コスト")]
    int _cost = 0;
    public int Cost
    {
        get { return _cost; }
#if UNITY_EDITOR
        set { _cost = value; }
#endif
    }
    [SerializeField,Tooltip("技")]
     List<CardSkill> _skillValue;
    public List<CardSkill> SkillValue
    {
        get { return _skillValue; }
#if UNITY_EDITOR
        set { _skillValue = value; }
#endif
    }

    [SerializeField, Tooltip("カードの属性")]
    Elements _element;
    public Elements Element
    {
        get { return _element; }
#if UNITY_EDITOR
        set { _element = value; }
#endif
    }

    [SerializeField, Tooltip("カードのスプライト")]
    Sprite _sprite;
    public Sprite Sprite
    {
        get { return _sprite; }
#if UNITY_EDITOR
        set { _sprite = value; }
#endif
    }

    [SerializeField, Tooltip("カードのタイプ")]
    PlayType _playType;
    public PlayType PlayType
    {
        get { return _playType; }
#if UNITY_EDITOR
        set { _playType = value; }
#endif
    }
}

/// <summary>
/// カードの属性
/// </summary>
public enum Elements
{
    Fire,
    Wood,
    Water
}

public enum PlayType
{
    None,
    Minion,
    Spell
}

