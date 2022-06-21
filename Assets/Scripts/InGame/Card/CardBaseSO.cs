using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBaseSO : ScriptableObject
{
    [SerializeField, Tooltip("カードの名前")]
    string _name;
    public string Name => _name;

    [SerializeField, Range(1, 2), Tooltip("技の数")]
    int _skillValue;
    public int SkillValue => _skillValue;

    [SerializeField, Tooltip("カードの属性")]
    Elements _element;
    public Elements Element => _element;

    [SerializeField, Tooltip("カードのスプライト")]
    Sprite _sprite;
    public Sprite Sprite => _sprite;
}

public enum Elements
{
    Fire,
    Wood,
    Water
}

