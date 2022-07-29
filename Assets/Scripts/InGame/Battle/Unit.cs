using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    //ひとまずメンバ変数に最大値を格納してる
    int _maxHP = 0;
    public int MaxHP => _maxHP;
    int _currentHP = 0;
    public int HP => _currentHP;

    int _maxMana = 0;
    public int MaxMana => _maxMana;
    int _mana = 0;
    public int Mana => _mana;

    public Unit(int initMaxHP,int initMaxMana)
    {
        _maxHP = initMaxHP;
        _currentHP = _maxHP;

        _maxMana = initMaxMana;
        _mana = 0;
    }

    /// <summary>
    /// HPを変化させる関数。戻り値は死亡時はtrue。
    /// </summary>
    /// <param name="value">現在のHPに加算される値。</param>
    public bool ChangeHP(int value)
    {
        _currentHP = Mathf.Clamp(_currentHP + value,0,_maxHP);
        if (_currentHP <=0)
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
        _mana = Mathf.Clamp(_mana +value,0,_maxMana);
    }
}
