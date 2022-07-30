using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ターンの遷移を制御するクラス
/// </summary>
public class TurnCycle : MonoBehaviour
{
    public enum EventEnum 
    {
        GameStart,
        MyTurnEnd,
        OpponentTurnEnd,
        Result
    }
    StateMachine<EventEnum> _stateMachine = null;

    void Awake()
    {
        BattleManager.Instance.Init();

        _stateMachine = new StateMachine<EventEnum>();

        _stateMachine.AddTransition<MyTurn, OpponentTurn>(EventEnum.MyTurnEnd,IsDeath);
        _stateMachine.AddTransition<OpponentTurn,MyTurn >(EventEnum.OpponentTurnEnd, IsDeath);

        _stateMachine.AddAnyTransitionTo<EndState>(EventEnum.Result);

        if (BattleManager.Instance.IsFirstTurn)
        {
            _stateMachine.StartSetUp<MyTurn>();
        }
        else
        {
            _stateMachine.StartSetUp<OpponentTurn>();
        }

        BattleManager.Instance.BattleUIManagerInstance.TurnEndButton.onClick.AddListener(() =>
        {
            _stateMachine.Dispatch(EventEnum.MyTurnEnd);
        });
        BattleManager.Instance.BattleUIManagerInstance.DebugOpponentTurnEndButton.onClick.AddListener(() =>
        {
            _stateMachine.Dispatch(EventEnum.OpponentTurnEnd);
        });
    }

    bool IsDeath()
    {
        return true;
    }

    class MyTurn :StateMachine<EventEnum>.State
    {
        public override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            BattleManager.Instance.PlayerTurnStart();
        }
    }

    class OpponentTurn : StateMachine<EventEnum>.State
    {
        public override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            BattleManager.Instance.EnemyTurnStart();
        }
    }

    class EndState : StateMachine<EventEnum>.State
    {

    }
}

