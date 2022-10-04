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
    StateMachine<EventEnum, TurnCycle> _stateMachine = null;

    void Awake()
    {
        BattleManager.Instance.Init();

        _stateMachine = new StateMachine<EventEnum,TurnCycle>(this);

        _stateMachine.AddTransition<MyTurn, OpponentTurn>(EventEnum.MyTurnEnd);
        _stateMachine.AddTransition<OpponentTurn,MyTurn >(EventEnum.OpponentTurnEnd);

        _stateMachine.AddAnyTransitionTo<EndState>(EventEnum.Result);

        if (BattleManager.Instance.IsFirstTurn)
        {
            _stateMachine.StartSetUp<MyTurn>();
        }
        else
        {
            _stateMachine.StartSetUp<OpponentTurn>();
        }

        BattleManager.Instance.TurnEndButton.onClick.AddListener(() =>
        {
            _stateMachine.Dispatch(EventEnum.MyTurnEnd);
        });

        BattleManager.Instance.OpponentBehavior.InitTask();
    }
    private void Update()
    {
        _stateMachine.Update();
    }

    public void ChangeState(EventEnum eventEnum)
    {
        _stateMachine.Dispatch(eventEnum);
    }

    class MyTurn :StateMachine<EventEnum, TurnCycle>.State
    {
        protected override void OnEnter(StateMachine<EventEnum, TurnCycle>.State prevState)
        {
            Debug.Log("MyTurn");
            BattleManager.Instance.PlayerTurnStart();
        }
    }

    class OpponentTurn : StateMachine<EventEnum, TurnCycle>.State
    {
        protected override void OnEnter(StateMachine<EventEnum, TurnCycle>.State prevState)
        {
            Debug.Log("OpponentTurn");
            BattleManager.Instance.EnemyTurnStart();
        }

        protected override void OnUpdate()
        {
            BattleManager.Instance.OpponentBehavior.OnUpdate();       
        }
    }

    class EndState : StateMachine<EventEnum, TurnCycle>.State
    {
        protected override void OnEnter(StateMachine<EventEnum, TurnCycle>.State prevState)
        {
            Debug.Log("終了");
        }
    }
}

