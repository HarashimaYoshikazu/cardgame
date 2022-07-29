using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI上のオブジェクトを生成、管理するクラス
/// </summary>
public class BattleUIManager : MonoBehaviour
{
    List<BattleCard> _cardObjectList = new List<BattleCard>();
    public List<BattleCard> CardObjects => _cardObjectList;

    GameObject _canvas = null;
    public GameObject Canvas => _canvas;

    GameObject _currentDrugParent = null;
    public GameObject CurrentDrugParent => _currentDrugParent;

    GameObject _currentPointerObject = null;
    /// <summary>現在ポインター上にあるオブジェクト</summary>
    public GameObject CurrentPointerObject
    {
        get => _currentPointerObject;
        set => _currentPointerObject = value;
    }
    /************
     相手側
     ***********/

    GameObject _opponentDeck = null;
    public GameObject OpponentDeck => _opponentDeck;

    GameObject _opponentField = null;
    public GameObject OpponentField => _opponentField;

    GameObject _opponentHands = null;
    public GameObject OpponentHands => _opponentHands;

    GameObject _opponentPlayerView = null;
    public GameObject OpponentPlayerView => _opponentPlayerView;
    Text _opponentHPText = null;
    public Text OpponentHPText => _opponentHPText;
    Text _opponentManaText = null;
    public Text OpponentManaText => _opponentManaText;

    /************
     自分側
      ***********/

    GameObject _ownDeck = null;
    public GameObject Oendeck => _ownDeck;

    GameObject _ownField = null;
    public GameObject OwnField => _ownField;

    GameObject _ownHands = null;
    public GameObject OwnHands => _ownHands;
  
    GameObject _ownPlayerView = null;
    public GameObject OwnPlayerView => _ownPlayerView;
    Text _ownHPText = null;
    public Text OwnHPText => _ownHPText;
    Text _ownManaText = null;
    public Text OwnManaText => _ownManaText;

    private void Awake()
    {
        SetUpUI();
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
            _opponentHPText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"),_opponentPlayerView.transform);
            _opponentManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerView.transform);
            Debug.Log(_opponentHPText);
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
            _ownHPText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerView.transform);
            _ownManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerView.transform);
            Debug.Log(_ownHPText);
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
