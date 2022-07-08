using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体を通して保存したい情報を制御するクラス
/// </summary>
public class GameManager : Singleton<GameManager>
{
    List<InventryCard> _inventryCards = new List<InventryCard>();

    /// <summary>インベントリのカードリスト</summary>
    List<InventryCard> InventryCards => _inventryCards;

    /// <summary>デッキのカードリスト</summary>
    List<InventryCard> _decksCards = new List<InventryCard>();

    int _cardLimit = 20;

    /// <summary>
    /// デッキのリストにカードを追加する関数
    /// </summary>
    /// <param name="card">追加したいカードクラス</param>
    public void AddCardToDeck(InventryCard card)
    {
        if (_decksCards.Count < _cardLimit)
        {
            card.SetIsDeck(true);
            _decksCards.Add(card);
        }
    }

    /// <summary>
    /// デッキのリストからカードを削除する関数
    /// </summary>
    /// <param name="card">削除したいカードクラス</param>
    public void RemoveCardToDeck(InventryCard card)
    {
        _decksCards.Remove(card);
    }

    /// <summary>
    /// インベントリのリストにカードを追加する関数
    /// </summary>
    /// <param name="card">追加したいカードクラス</param>
    public void AddCardToInventry(InventryCard card)
    {
        _inventryCards.Add(card);
    }

    /// <summary>
    /// インベントリのリストからカードを削除する関数
    /// </summary>
    /// <param name="card">削除したいカードクラス</param>
    public void RemoveCardToInventry(InventryCard card)
    {
        _inventryCards.Remove(card);
    }

    GameCycle _gameCycle = null;
    public GameCycle GameCycle
    {
        get
        {
            if (!_gameCycle)
            {
                GameObject go = new GameObject();
                go.name = "GameCycle";
                var deckCustom = go.AddComponent<GameCycle>();
                _gameCycle = deckCustom;
            }
            return _gameCycle;
        }
        set { _gameCycle = value; }
    }

}
