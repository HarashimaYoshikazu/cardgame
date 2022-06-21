using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCycle : MonoBehaviour
{
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

    class StartState : StateMachine<EventEnum>.State
    {
        protected override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            GameManager.Instance.SetUp();
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

