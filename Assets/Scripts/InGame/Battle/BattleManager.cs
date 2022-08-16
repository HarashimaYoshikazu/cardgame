using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleManager : Singleton<BattleManager>
{
    /// オンライン上では情報クラスは1つしかもたない。
    UnitData _player = null;
    /// <summary>プレイヤーの情報クラス</summary>
    public UnitData Player => _player;
    UnitData _enemy = null;
    /// <summary>敵の情報クラス</summary>
    public UnitData Enemy => _enemy;

    //初期化設定
    public int FirstHands => BattleManagerAttachment.FirstHands;
    public int HandsLimit => BattleManagerAttachment.HandsLimit;

    BattleManagerAttachment _battleManagerAttachment = null;
    /// <summary>バトルの設定をインスペクターから設定するクラス</summary>
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
    /// <summary>ターン遷移を制御するクラスのインスタンス</summary>
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

    /// <summary>どちらのプレイヤーが先行かのフラグ（デバッグ用）</summary>
    public bool IsFirstTurn { get { return BattleManagerAttachment.IsFirstTurn; } }

    bool _isMyTurn = false;
    /// <summary>現在のターンが自分かどうか</summary>
    public bool IsMyTurn
    {
        get { return _isMyTurn; }
        set { _isMyTurn = value; }
    }

    public void Init()
    {
        if (GameManager.Instance.DeckCards.Length == 0)
        {
            for (int i = 0; i < GameManager.Instance.CardLimit; i++)
            {
                GameManager.Instance.AddCardToDeck(1);
            }
        }

        _player = new UnitData(20, BattleUIManagerInstance.OwnHPText, BattleUIManagerInstance.OwnManaText, BattleUIManagerInstance.OwnMaxManaText, GameManager.Instance.DeckCards, UnitType.Player);
        int[] enemyDeck = GameManager.Instance.DeckCards;
        _enemy = new UnitData(20, BattleUIManagerInstance.OpponentHPText, BattleUIManagerInstance.OpponentCurrentManaText, BattleUIManagerInstance.OpponentMaxManaText, enemyDeck, UnitType.Opponent);
        DistributeHands(_player);
        DistributeHands(_enemy);
    }

    void DistributeHands(UnitData unit)
    {
        for (int i = 0; i < FirstHands; i++)
        {
            int rand = Random.Range(0, unit.Deck.Length - 1);
            int cardID = unit.Deck[rand];
            DrawCard(unit, cardID);
        }
    }

    private void DrawCard(UnitData unit, int cardID)
    {
        unit.AddHands(cardID);
        unit.RemoveDeck(cardID);
        _battleUIManager.CreateHandsObject(unit.Type, cardID);
    }

    //ソロプレイ想定
    const int _addMana = 1;
    public void PlayerTurnStart()
    {
        int rand = Random.Range(0, _player.Deck.Length - 1);
        int cardID = _player.Deck[rand];
        DrawCard(_player, cardID);
        Player.ChangeMaxMana(_addMana);
        Player.ResetCurrentMana();
        _isMyTurn = true;
    }
    public void EnemyTurnStart()
    {
        int rand = Random.Range(0, _enemy.Deck.Length - 1);
        int cardID = _enemy.Deck[rand];
        DrawCard(_enemy, cardID);//ランダムなカードを引く
        Enemy.ChangeMaxMana(_addMana);//マナを増やす
        Enemy.ResetCurrentMana();//マナ回復
        _isMyTurn = false;
    }
}
