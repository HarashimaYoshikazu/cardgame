using UnityEngine;
public class CardData
{
    /// <summary>カードの名前</summary>
    string _name;
    public string Name => _name;

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
        _skillValue = cardBaseSO.SkillValue;
        _element = cardBaseSO.Element;
        _sprite = cardBaseSO.Sprite;
    }
}