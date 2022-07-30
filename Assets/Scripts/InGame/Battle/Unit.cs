using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Unit
{
    //ひとまずメンバ変数に最大値を格納してる
    int _maxHP = 0;
    public int MaxHP => _maxHP;

    ReactiveProperty<int> _currentHP = new ReactiveProperty<int>();
    public int CurrentHP => _currentHP.Value;

    int _maxMana = 0;
    public int MaxMana => _maxMana;
    ReactiveProperty<int> _currentMana = new ReactiveProperty<int>();
    public int CurrentMana => _currentMana.Value;

    public Unit(int initMaxHP,int initMaxMana,Text hpText,Text manaText)
    {
        _currentHP.Subscribe(x =>
        {
            hpText.text = x.ToString();
            Debug.Log($"HP{x}");
        });

        _currentMana.Subscribe(x =>
        {
            manaText.text = x.ToString();
            Debug.Log($"マナ{x}");
        });

        _maxHP = initMaxHP;
        _currentHP.Value =_maxHP;

        _maxMana = initMaxMana;
        _currentMana.Value = 0;


    }

    /// <summary>
    /// HPを変化させる関数。戻り値は死亡時はtrue。
    /// </summary>
    /// <param name="value">現在のHPに加算される値。</param>
    public bool ChangeHP(int value)
    {
        _currentHP.Value = Mathf.Clamp(_currentHP.Value + value,0,_maxHP);
        if (_currentHP.Value <=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// マナを変化させる関数
    /// </summary>
    public void ChangeMana(int value)
    {
        _currentMana.Value = Mathf.Clamp(_currentMana.Value +value,0,_maxMana);
    }
}
