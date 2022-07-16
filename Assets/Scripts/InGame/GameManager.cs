using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体を通して保存したい情報を制御するクラス
/// </summary>
public class GameManager : Singleton<GameManager>
{
    List<int> _inventryCards = new List<int>();

    /// <summary>インベントリのカードリスト</summary>
    List<int> InventryCards => _inventryCards;

    /// <summary>デッキのカードリスト</summary>
    List<int> _decksCards = new List<int>();
    public List<int> DeckCards => _decksCards;

    int _cardLimit = 20;

    /// <summary>
    /// デッキのリストにカードを追加する関数
    /// </summary>
    /// <param name="cardID">追加したいカードクラス</param>
    public void AddCardToDeck(int cardID)
    {
        if (_decksCards.Count < _cardLimit)
        {
            _decksCards.Add(cardID);
        }
    }

    /// <summary>
    /// デッキのリストからカードを削除する関数
    /// </summary>
    /// <param name="cardID">削除したいカードクラス</param>
    public void RemoveCardToDeck(int cardID)
    {
        _decksCards.Remove(cardID);
    }

    /// <summary>
    /// インベントリのリストにカードを追加する関数
    /// </summary>
    /// <param name="cardID">追加したいカードクラス</param>
    public void AddCardToInventry(int cardID)
    {
        _inventryCards.Add(cardID);
    }

    /// <summary>
    /// インベントリのリストからカードを削除する関数
    /// </summary>
    /// <param name="cardID">削除したいカードクラス</param>
    public void RemoveCardToInventry(int cardID)
    {
        _inventryCards.Remove(cardID);
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
