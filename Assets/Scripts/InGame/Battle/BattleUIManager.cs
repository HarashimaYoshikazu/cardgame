using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIManager : MonoBehaviour
{
    List<BattleCard> _handsObjectList = new List<BattleCard>();

    [SerializeField]
    GameObject _opponentDeck = null;
    public GameObject OpponentDeck => _opponentDeck;
    [SerializeField]
    GameObject _opponentField = null;
    public GameObject OpponentField => _opponentField;
    [SerializeField]
    GameObject _opponentHands = null;
    public GameObject OpponentHands => _opponentHands;
    [SerializeField]
    GameObject _opponentPlayerView = null;
    public GameObject OpponentPlayerView => _opponentPlayerView;

    [SerializeField]
    GameObject _ownDeck = null;
    public GameObject Oendeck => _ownDeck;
    [SerializeField]
    GameObject _ownField = null;
    public GameObject OwnField => _ownField;
    [SerializeField]
    GameObject _ownHands = null;
    public GameObject OwnHands => _ownHands;
    [SerializeField]
    GameObject _ownPlayerView = null;
    public GameObject OwnPlayerView => _ownPlayerView;

    GameObject _currentDrugParent = null;
    public GameObject CurrentDrugParent => _currentDrugParent;

    public void SetUpUI()
    {
        GameObject canvas = Instantiate(Resources.Load<GameObject>("UIPrefabs/Canvas"));
        if (!_opponentDeck)
        {
            _opponentDeck =Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentDeck"),canvas.transform)  ;

        }
        if (!_opponentField)
        {
            _opponentField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentField"), canvas.transform);
        }
        if (!_opponentHands)
        {
            _opponentHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentHands"), canvas.transform);
        }
        if (!_opponentPlayerView)
        {
            _opponentPlayerView = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentPlayerView"), canvas.transform);
        }

        if (!_ownDeck)
        {
            _ownDeck = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownDeck"), canvas.transform);
        }
        if (!_ownField)
        {
            _ownField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownField"), canvas.transform);
        }
        if (!_ownHands)
        {
            _ownHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownHands"), canvas.transform);
        }
        if (!_ownPlayerView)
        {
            _ownPlayerView = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownPlayerView"), canvas.transform);
        }
        if (!_currentDrugParent)
        {
            _currentDrugParent = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/CurrentDrugParent"),canvas.transform);
        }
    }

    public void CreateHandsObject(int cardID)
    {
        var goPrefab = Resources.Load<GameObject>($"CardPrefab/Battle/Card{cardID}");
        var go = Instantiate(goPrefab, _ownHands.transform);

        var card = go.GetComponent<BattleCard>();
        _handsObjectList.Add(card);
    }
}
