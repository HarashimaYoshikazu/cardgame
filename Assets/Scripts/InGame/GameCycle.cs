using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCycle : MonoBehaviour
{
    //ÉQÅ[ÉÄÇÃèÛë‘ä«óù

    enum GameStateEvent
    {
        GoBattle,
        GoHome,
    }

    StateMachine<GameStateEvent> _gameState = new StateMachine<GameStateEvent>();
    private void Awake()
    {
        //èâä˙âªëJà⁄
        _gameState.AddTransition<Empty, HomeScene>(GameStateEvent.GoHome);
        _gameState.AddTransition<Empty, BattleScene>(GameStateEvent.GoBattle);

        //ÉQÅ[ÉÄì‡ëJà⁄
        _gameState.AddTransition<TitleScene, HomeScene>(GameStateEvent.GoHome);
        _gameState.AddTransition<HomeScene, BattleScene>(GameStateEvent.GoBattle);
        _gameState.AddTransition<BattleScene, HomeScene>(GameStateEvent.GoHome);

        _gameState.StartSetUp<Empty>();
        GameManager.Instance.GameCycle = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneChange;
    }

    //FIXMEÅ@ifÇ≈ìùçáÇ≈Ç´ÇÈ
    void OnSceneChange(Scene nextScene, LoadSceneMode mode)
    {
        switch (_gameState.CurrentState)
        {
            case Empty:
                if (SceneManager.GetActiveScene().name =="Home")
                {
                    _gameState.Dispatch(GameStateEvent.GoHome);
                }
                else if (SceneManager.GetActiveScene().name == "Battle")
                {
                    _gameState.Dispatch(GameStateEvent.GoBattle);
                }
                break;
            case HomeScene:
                _gameState.Dispatch(GameStateEvent.GoBattle);
                break;

            case BattleScene:
                _gameState.Dispatch(GameStateEvent.GoHome);
                break;
        }

    }

    public void GoBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
    }

    public void GoHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    class TitleScene : StateMachine<GameStateEvent>.State
    {

    }

    class HomeScene : StateMachine<GameStateEvent>.State
    {
        public override void OnEnter(StateMachine<GameStateEvent>.State prevState)
        {
            HomeManager.Instance.DeckCustomUIManager.SetUpUIObject();
        }

        protected override void OnExit(StateMachine<GameStateEvent>.State nextState)
        {
            
        }
    }

    class BattleScene : StateMachine<GameStateEvent>.State
    {
        public override void OnEnter(StateMachine<GameStateEvent>.State prevState)
        {
            BattleManager.Instance.SetUpCards();
        }

        protected override void OnExit(StateMachine<GameStateEvent>.State nextState)
        {
            
        }
    }
    class Empty : StateMachine<GameStateEvent>.State
    {

    }
}
