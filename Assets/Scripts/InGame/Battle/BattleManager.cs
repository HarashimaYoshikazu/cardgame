using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    TurnCycle _turnCycleInstance = null;
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
}
