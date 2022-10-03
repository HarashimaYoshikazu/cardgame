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
                var tc = GameObject.FindObjectOfType<TurnCycle>();
                if (tc)
                {
                    _turnCycleInstance = tc;
                }
                else
                {
                    var go = new GameObject();
                    go.name = "TurnCycle";
                    _turnCycleInstance = go.AddComponent<TurnCycle>();
                }
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
                var go = new GameObject("BattleUIManager");
                _battleUIManager = go.AddComponent<BattleUIManager>();
            }
            return _battleUIManager;
        }
        set { _battleUIManager = value; }
    }

    OpponentBehavior _opponentBehavior = null;
    public OpponentBehavior OpponentBehavior
    {
        get
        {
            if (!_opponentBehavior)
            {
                var ob = GameObject.FindObjectOfType<OpponentBehavior>();
                if (ob)
                {
                    _opponentBehavior = ob;
                }
                else
                {
                    var go = new GameObject("OpponentBehavior");
                    _opponentBehavior = go.AddComponent<OpponentBehavior>();
                }
            }
            return _opponentBehavior;
        }
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
        BattleUIManagerInstance.SetUpUI();
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

    public void DrawCard(UnitType unitType)
    {
        UnitData unit = null;
        switch (unitType)
        {
            case UnitType.Player:
                unit = _player;
                break;
            case UnitType.Opponent:
                unit = _enemy;
                break;
        }
        if (unit == null)
        {
            Debug.LogError($"UnitTypeのパラメータが不正な値です。：{unitType}");
        }
        else
        {
            int cardID = unit.GetRandomDeckCardID;
            if (cardID != -1)
            {
                DrawCard(unit, cardID);
            }        
        }
    }

    public void DrawCard(UnitData unit, int cardID)
    {
        unit.AddHands(cardID);
        unit.RemoveDeck(cardID);
        _battleUIManager.CreateHandsObject(unit.Type, cardID);
    }

    public void PlayCard(UnitType unitType, int cardID)
    {
        UnitData unit = null;
        BattleCard[] battleCards = null;
        switch (unitType)
        {
            case UnitType.Player:
                unit = _player;
                battleCards = _battleUIManager.OwnHandCards;
                break;
            case UnitType.Opponent:
                unit = _enemy;
                battleCards = _battleUIManager.OpponentHandCards;
                break;
        }
        if (unit == null)
        {
            Debug.LogError($"UnitTypeのパラメータが不正な値です。：{unitType}");
        }
        else
        {
            unit.AddFields(cardID);
            unit.RemoveHands(cardID);
            _battleUIManager.AddField(unit.Type,battleCards,cardID);
            _battleUIManager.RemoveHand(unit.Type, battleCards, cardID);
        }
    }

    //ソロプレイ想定
    const int _addMana = 1;
    public void PlayerTurnStart()
    {
        DrawCard(UnitType.Player);
        Player.ChangeMaxMana(_addMana);
        Player.ResetCurrentMana();
        _isMyTurn = true;
    }
    public void EnemyTurnStart()
    {
        DrawCard(UnitType.Opponent);//ランダムなカードを引く
        Enemy.ChangeMaxMana(_addMana);//マナを増やす
        Enemy.ResetCurrentMana();//マナ回復
        _opponentBehavior.SelectTasks();
        _isMyTurn = false;
    }
}
