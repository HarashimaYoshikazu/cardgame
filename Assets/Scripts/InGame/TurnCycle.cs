using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCycle : MonoBehaviour
{
    enum EventEnum 
    {

    }
    StateMachine<EventEnum> _stateMachine = null;

    private void Awake()
    {
        _stateMachine = new StateMachine<EventEnum>();
    }


}

