using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _opponentDeck = null;
    [SerializeField]
    GameObject _opponentField = null;
    [SerializeField]
    GameObject _opponentHands = null;
    [SerializeField]
    GameObject _opponentPlayerView = null;

    [SerializeField]
    GameObject _ownDeck = null;
    [SerializeField]
    GameObject _ownField = null;
    [SerializeField]
    GameObject _ownHands = null;
    [SerializeField]
    GameObject _ownPlayerView = null;

    public void SetUpUI()
    {
        List<GameObject> list = new List<GameObject>();
        if (!_opponentDeck)
        {
            list.Add(_opponentDeck = Resources.Load<GameObject>("UIPrefabs/Battle/opponentDeck"));
        }
        if (!_opponentField)
        {
            list.Add(_opponentField = Resources.Load<GameObject>("UIPrefabs/Battle/opponentField")) ;
        }
        if (!_opponentHands)
        {
             list.Add(_opponentHands = Resources.Load<GameObject>("UIPrefabs/Battle/opponentHands"));
        }
        if (!_opponentPlayerView)
        {
             list.Add(_opponentPlayerView = Resources.Load<GameObject>("UIPrefabs/Battle/opponentPlayerView"));
        }

        if (!_ownDeck)
        {
             list.Add(_ownDeck = Resources.Load<GameObject>("UIPrefabs/Battle/ownDeck"));
        }
        if (!_ownField)
        {
             list.Add(_ownField = Resources.Load<GameObject>("UIPrefabs/Battle/ownField"));
        }
        if (!_ownHands)
        {
             list.Add(_ownHands = Resources.Load<GameObject>("UIPrefabs/Battle/ownHands"));
        }
        if (!_ownPlayerView)
        {
             list.Add(_ownPlayerView = Resources.Load<GameObject>("UIPrefabs/Battle/ownPlayerView"));
        }

       GameObject canvas = Instantiate(Resources.Load<GameObject>("UIPrefabs/Canvas")) ;
        if (list.Count ==0)
        {
            return;
        }
        foreach (var i in list)
        {
            Instantiate(i, canvas.transform);
        }
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
