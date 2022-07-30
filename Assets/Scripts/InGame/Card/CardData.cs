using UnityEngine;
public class CardData
{
    /// <summary>カードの名前</summary>
    string _name;
    public string Name => _name;

    int _attack = 0;
    public int Attack => _attack;

    int _hp = 0;
    public int HP => _hp;

    int _cost = 0;
    public int Cost => _cost;

    /// <summary>カードの技の数</summary>
    int _skillValue;
    public int SkillValue => _skillValue;

    /// <summary>カードの属性</summary>
    Elements _element;
    public Elements Element => _element;

    /// <summary>カードのスプライト</summary>
    Sprite _sprite;
    public Sprite Sprite => _sprite;

    public CardData(int cardID)
    {
        CardBaseSO cardBaseSO = Resources.Load<CardBaseSO>("CardSO/Card" + cardID);
        _name = cardBaseSO.Name;
        _attack = cardBaseSO.Attack;
        _hp = cardBaseSO.HP;
        _cost = cardBaseSO.Cost;
        _skillValue = cardBaseSO.SkillValue;
        _element = cardBaseSO.Element;
        _sprite = cardBaseSO.Sprite;
    }
}