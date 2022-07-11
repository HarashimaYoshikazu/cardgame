using UnityEngine;

/// <summary>
/// ターンの遷移を制御するクラス
/// </summary>
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

    void Awake()
    {
        _stateMachine = new StateMachine<EventEnum>();

        _stateMachine.AddTransition<StartState, MyTurn>(EventEnum.GameStart,IsDeath);
        _stateMachine.AddTransition<MyTurn, OpponentTurn>(EventEnum.MyTurnEnd,IsDeath);
        _stateMachine.AddTransition<OpponentTurn,MyTurn >(EventEnum.OpponentTurnEnd, IsDeath);

        _stateMachine.AddAnyTransitionTo<EndState>(EventEnum.Result);

        _stateMachine.StartSetUp<StartState>();
    }

    int hp = 0;
    bool IsDeath()
    {
        if (hp<1)
        {
            return true;
        }
        return false;
    }

    class StartState : StateMachine<EventEnum>.State
    {
        public override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            
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

