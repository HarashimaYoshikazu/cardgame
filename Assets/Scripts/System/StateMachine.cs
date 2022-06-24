using System.Collections.Generic;
using System;

/// ステートマシンクラス
public class StateMachine<Event> where Event : System.Enum
{
    /// <summary>
    /// ステートを表すクラス
    /// </summary>
    public abstract class State
    {
        protected StateMachine<Event> StateMachine => stateMachine;
        internal StateMachine<Event> stateMachine;

        internal Dictionary<Event, State> transitions = new Dictionary<Event, State>();


        internal void Enter(State prevState)
        {
            OnEnter(prevState);
        }
        protected virtual void OnEnter(State prevState) { }


        internal void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate() { }

        internal void Exit(State nextState)
        {
            OnExit(nextState);
        }

        protected virtual void OnExit(State nextState) { }
    }

    /// <summary>
    /// どのステートからでも特定のステートへ遷移できるようにするための仮想ステート
    /// </summary>
    public sealed class AnyState : State { }

    /// <summary>
    /// 現在のステート
    /// </summary>
    public State CurrentState { get; private set; }

    // ステートリスト
    private LinkedList<State> states = new LinkedList<State>();


    public T Add<T>() where T : State, new()
    {
        var state = new T();
        state.stateMachine = this;
        states.AddLast(state);
        return state;
    }


    public T GetOrAddState<T>() where T : State, new()
    {
        foreach (var state in states)
        {
            if (state is T result)
            {
                return result;
            }
        }
        return Add<T>();
    }

    /// <param name="eventId">イベントID</param>
    public void AddTransition<TFrom, TTo>(Event eventId,Func<bool> isTransition)
        where TFrom : State, new()
        where TTo : State, new()
    {
        if (isTransition())
        {
            var from = GetOrAddState<TFrom>();
            if (from.transitions.ContainsKey(eventId))
            {
                // 同じイベントIDの遷移を定義済
                throw new System.ArgumentException(
                    $"ステート'{nameof(TFrom)}'に対してイベントID'{eventId.ToString()}'の遷移は定義済です");
            }

            var to = GetOrAddState<TTo>();
            from.transitions.Add(eventId, to);
        }

    }


    public void AddAnyTransition<TTo>(Event eventId) where TTo : State, new()
    {
        AddTransition<AnyState, TTo>(eventId,()=>true);
    }


    public void StartSetUp<TFirst>() where TFirst : State, new()
    {
        Start(GetOrAddState<TFirst>());
    }


    public void Start(State firstState)
    {
        CurrentState = firstState;
        CurrentState.Enter(null);
    }


    public void Update()
    {
        CurrentState.Update();
    }


    /// <param name="eventId">イベントID</param>
    public void Dispatch(Event eventId)
    {
        State to;
        if (!CurrentState.transitions.TryGetValue(eventId, out to))
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
            {
                // イベントに対応する遷移が見つからなかった
                return;
            }
        }
        Change(to);
    }

    private void Change(State nextState)
    {
        CurrentState.Exit(nextState);
        nextState.Enter(CurrentState);
        CurrentState = nextState;
    }
}

