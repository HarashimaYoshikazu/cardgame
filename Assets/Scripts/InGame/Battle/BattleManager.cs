using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    /// <summary>山札</summary>
    List<int> _deck = new List<int>();
    public void AddDeck(int cardID) { _deck.Add(cardID); }
    public void RemoveDeck(int cardID) { _deck.RemoveAt(cardID); }

    /// <summary>手札</summary>
    List<int> _hands = new List<int>();
    public void AddHands(int cardID) { _hands.Add(cardID); }
    public void RemoveHands(int cardID) { _hands.Remove(cardID); }

    //初期化設定
    public int FirstHands => BattleManagerAttachment.FirstHands;
    public int HandsLimit => BattleManagerAttachment.HandsLimit;

    BattleManagerAttachment _battleManagerAttachment = null;
    public BattleManagerAttachment BattleManagerAttachment
    {
        get
        {
            if (!_battleManagerAttachment)
            {
                var bm = GameObject.FindObjectOfType<BattleManagerAttachment>();
                if (!bm)
                {
                    throw new System.ArgumentNullException();
                }
                else
                {
                    _battleManagerAttachment = bm;
                }
            }
            return _battleManagerAttachment;
        }
        set { _battleManagerAttachment = value; }
    }

    TurnCycle _turnCycleInstance = null;
    public TurnCycle TurnCycleInstance
    {
        get
        {
            if (!_turnCycleInstance)
            {
                var go = new GameObject();
                go.name = "TurnCycle";
                var tc = go.AddComponent<TurnCycle>();
                _turnCycleInstance = tc;
            }
            return _turnCycleInstance;
        }
        set { _turnCycleInstance = value; }
    }

    BattleUIManager _battleUIManager = null;
    public BattleUIManager BattleUIManagerInstance
    {
        get
        {
            if (!_battleUIManager)
            {
                var go = new GameObject();
                go.name = "BattleUIManager";
                _battleUIManager = go.AddComponent<BattleUIManager>();
            }
            return _battleUIManager;
        }
        set { _battleUIManager = value; }
    }

    public void SetUpCards()
    {
        BattleUIManagerInstance.SetUpUI();

        _deck = GameManager.Instance.DeckCards;
        DistributeHands();
    }

    void DistributeHands()
    {
        //デバッグ
        if (_deck.Count ==0)
        {
            for (int i = 0;i<GameManager.Instance.CardLimit;i++)
            {
                GameManager.Instance.AddCardToDeck(1);
            }            
        }

        if (_hands.Count <= HandsLimit && _deck.Count > 0)
        {
            for (int i = 0; i < FirstHands; i++)
            {
                int rand = Random.Range(0, _deck.Count);
                int cardID = _deck[rand];
                _hands.Add(cardID);
                _deck.Remove(cardID);
                _battleUIManager.CreateHandsObject(cardID);
            }
        }
    }
}
