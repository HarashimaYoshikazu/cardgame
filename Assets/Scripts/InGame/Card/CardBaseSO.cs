using UnityEngine;
using System;

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

    [SerializeField, Range(1, 2), Tooltip("技の数")]
    int _skillValue;
    public int SkillValue
    {
        get { return _skillValue; }
#if UNITY_EDITOR
        set { _skillValue = Mathf.Clamp(value, 1, 2); }
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

