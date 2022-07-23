using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerAttachment : MonoBehaviour
{
    [SerializeField]
    int _firstHands = 4;
    public int FirstHands => _firstHands;

    [SerializeField]
    int _handsLimit = 10;
    public int HandsLimit => _handsLimit;

    [SerializeField]
    bool _isFirstTurn = true;
    public bool IsFirstTurn => _isFirstTurn;
}
