using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCycle : MonoBehaviour
{
    List<InventryCard> _inventryCards = new List<InventryCard>();
    List<InventryCard> _decksCards = new List<InventryCard>();
    enum EventEnum 
    {
        GameStart,
        MyTurnEnd,
        OpponentTurnEnd,
        Result
    }
    StateMachine<EventEnum> _stateMachine = null;

    private void Awake()
    {
        _stateMachine = new StateMachine<EventEnum>();

        _stateMachine.StartSetUp<StartState>();
    }

    private void Update()
    {
        _inventryCards = GameManager.Instance._inventryCards;
        _decksCards = GameManager.Instance._decksCards;
    }

    class StartState : StateMachine<EventEnum>.State
    {
        protected override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            HomeManager.Instance.DeckCustomUIManager.UISetUp();
        }
    }

    class MyTurn :StateMachine<EventEnum>.State
    {

    }

    class OpponentTurn : StateMachine<EventEnum>.State
    {

    }

    class EndState : StateMachine<EventEnum>.State
    {

    }


}

