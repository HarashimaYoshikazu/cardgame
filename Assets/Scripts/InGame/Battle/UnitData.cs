using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Linq;

public class UnitData
{
    //ひとまずメンバ変数に最大値を格納してる
    int _maxHP = 0;
    public int MaxHP => _maxHP;

    ReactiveProperty<int> _currentHP = new ReactiveProperty<int>();
    public int CurrentHP => _currentHP.Value;

    ReactiveProperty<int> _maxMana = new ReactiveProperty<int>();
    public int MaxMana => _maxMana.Value;
    ReactiveProperty<int> _currentMana = new ReactiveProperty<int>();
    public int CurrentMana => _currentMana.Value;

    /// <summary>山札</summary>
    List<int> _deck = new List<int>();
    public int[] Deck => _deck.ToArray();
    public void AddDeck(int cardID) { _deck.Add(cardID); }
    public void RemoveDeck(int cardID) { _deck.RemoveAt(cardID); }

    /// <summary>手札</summary>
    List<int> _hands = new List<int>();
    public int[] Hands => _hands.ToArray();
    public void AddHands(int cardID) { _hands.Add(cardID); }
    public void RemoveHands(int cardID) { _hands.Remove(cardID); }

    UnitType _type;
    public UnitType Type => _type;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="initMaxHP">最大HP</param>
    /// <param name="currenthpText">現在のHPのView</param>
    /// <param name="currentmanaText">現在のマナのView</param>
    /// <param name="maxmanaText">最大マナのView</param>
    public UnitData(int initMaxHP,Text currenthpText,Text currentmanaText,Text maxmanaText,int[] deck,UnitType unitType)
    {
        _currentHP.Subscribe(x =>
        {
            currenthpText.text = x.ToString();
            Debug.Log($"現在のHP{x}");
        });

        _currentMana.Subscribe(x =>
        {
            currentmanaText.text = x.ToString();
            Debug.Log($"現在のマナ{x}");
        });

        _maxMana.Subscribe(x =>
        {
            maxmanaText.text = x.ToString();
        });

        _maxHP = initMaxHP;
        _currentHP.Value =_maxHP;

        _maxMana.Value = 0;
        _currentMana.Value = 0;

        _deck = deck.ToList();
        _type = unitType;
    }


    /// <summary>
    /// HPを変化させる関数。戻り値は死亡時はtrue。
    /// </summary>
    /// <param name="value">現在のHPに加算される値。</param>
    public void ChangeCurrentHP(int value)
    {
        _currentHP.Value = Mathf.Clamp(_currentHP.Value + value,0,_maxHP);
        if (_currentHP.Value <= 0)
        {
            BattleManager.Instance.TurnCycleInstance.ChangeState(TurnCycle.EventEnum.Result);
        }
    }

    /// <summary>
    /// 現在のマナを変化させる関数
    /// </summary>
    public void ChangeCurrentMana(int value)
    {
        _currentMana.Value = Mathf.Clamp(_currentMana.Value +value,0,_maxMana.Value);
    }

    public void ResetCurrentMana()
    {
        _currentMana.Value = _maxMana.Value;
    }

    public void ChangeMaxMana(int value)
    {
        _maxMana.Value += value;
    }
}
public enum UnitType
{
    None,
    Player,
    Opponent
}
