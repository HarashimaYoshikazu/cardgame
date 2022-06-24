using UnityEngine;
using System;

[CreateAssetMenu, Serializable]
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

public enum Elements
{
    Fire,
    Wood,
    Water
}

