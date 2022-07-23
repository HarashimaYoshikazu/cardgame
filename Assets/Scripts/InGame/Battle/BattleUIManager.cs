using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI上のオブジェクトを生成、管理するクラス
/// </summary>
public class BattleUIManager : MonoBehaviour
{
    List<BattleCard> _cardObjectList = new List<BattleCard>();
    public List<BattleCard> CardObjects => _cardObjectList;

    GameObject _canvas = null;
    public GameObject Canvas => _canvas;

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
   
    GameObject _currentPointerObject = null;
    /// <summary>現在ポインター上にあるオブジェクト</summary>
    public GameObject CurrentPointerObject
    {
        get => _currentPointerObject;
        set => _currentPointerObject = value;
    }

    /// <summary>
    /// Battleシーンで使うUIオブジェクトを生成
    /// </summary>
    public void SetUpUI()
    {
        _canvas = Instantiate(Resources.Load<GameObject>("UIPrefabs/Canvas"));
        if (!_opponentDeck)
        {
            _opponentDeck =Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentDeck"),_canvas.transform)  ;

        }
        if (!_opponentField)
        {
            _opponentField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentField"), _canvas.transform);
        }
        if (!_opponentHands)
        {
            _opponentHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentHands"), _canvas.transform);
        }
        if (!_opponentPlayerView)
        {
            _opponentPlayerView = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentPlayerView"), _canvas.transform);
        }

        if (!_ownDeck)
        {
            _ownDeck = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownDeck"), _canvas.transform);
        }
        if (!_ownField)
        {
            _ownField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownField"), _canvas.transform);
        }
        if (!_ownHands)
        {
            _ownHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownHands"), _canvas.transform);
        }
        if (!_ownPlayerView)
        {
            _ownPlayerView = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownPlayerView"), _canvas.transform);
        }
        if (!_currentDrugParent)
        {
            _currentDrugParent = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/CurrentDrugParent"),_canvas.transform);
            _currentDrugParent.transform.parent.SetAsLastSibling();
        }
    }

    /// <summary>
    /// IDに応じたカードをUI上に生成する
    /// </summary>
    /// <param name="cardID"></param>
    public void CreateHandsObject(int cardID)
    {
        var battleCardPrefab = Resources.Load<GameObject>($"CardPrefab/Battle/Card{cardID}");
        var battleCard = Instantiate(battleCardPrefab, _ownHands.transform);
        _cardObjectList.Add(battleCard.GetComponent<BattleCard>());
    }
}
