using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CardData
{
    /// <summary>カードの名前</summary>
    string _name;
    public string Name => _name;

    ReactiveProperty<int> _attack = new ReactiveProperty<int>();
    public int Attack => _attack.Value;

    int _maxHP = 0;
    public int MaxHP => _maxHP;

    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    public int CurrentHP => _hp.Value;

    ReactiveProperty<int> _cost = new ReactiveProperty<int>();
    public int Cost => _cost.Value;

    /// <summary>カードの技の数</summary>
    int _skillValue;
    public int SkillValue => _skillValue;

    /// <summary>カードの属性</summary>
    Elements _element;
    public Elements Element => _element;

    /// <summary>カードのスプライト</summary>
    Sprite _sprite;
    public Sprite Sprite => _sprite;

    public CardData(int cardID,Text attackText,Text hpText,Text costText,GameObject card)
    {
        _attack.Subscribe(x => {attackText.text = x.ToString(); }).AddTo(card);
        _hp.Subscribe(x => { hpText.text = x.ToString(); }).AddTo(card);
        _cost.Subscribe(x => { costText.text = x.ToString(); }).AddTo(card);

        CardBaseSO cardBaseSO = Resources.Load<CardBaseSO>("CardSO/Card" + cardID);
        _name = cardBaseSO.Name;
        _attack.Value = cardBaseSO.Attack;
        _maxHP = cardBaseSO.MAXHP;
        _hp.Value = _maxHP;
        _cost.Value = cardBaseSO.Cost;
        _skillValue = cardBaseSO.SkillValue;
        _element = cardBaseSO.Element;
        _sprite = cardBaseSO.Sprite;
    }
}