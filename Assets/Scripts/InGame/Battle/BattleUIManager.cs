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
    Text _opponentCurrentManaText = null;
    public Text OpponentCurrentManaText => _opponentCurrentManaText;
    Text _opponentMaxManaText = null;
    public Text OpponentMaxManaText => _opponentMaxManaText;

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
    Text _ownMaxManaText = null;
    public Text OwnMaxManaText => _ownMaxManaText;


    /*
     * ボタン
     * */
    Button _turnEndButton = null;
    public Button TurnEndButton => _turnEndButton;

    Button _debugOpponentTurnEndButton = null;
    public Button DebugOpponentTurnEndButton => _debugOpponentTurnEndButton;

    private void Awake()
    {
        SetUpUI();
    }

    /// <summary>
    /// Battleシーンで使うUIオブジェクトを生成
    /// </summary>
    public void SetUpUI()
    {
        //キャンバス
        _canvas = Instantiate(Resources.Load<GameObject>("UIPrefabs/Canvas"));

        //敵デッキ
        _opponentDeck = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentDeck"), _canvas.transform);

        //敵フィールド
        _opponentField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentField"), _canvas.transform);

        //敵手札
        _opponentHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentHands"), _canvas.transform);

        //敵情報
        _opponentPlayerView = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentPlayerView"), _canvas.transform);
        _opponentHPText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerView.transform);
        _opponentCurrentManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerView.transform);
        _opponentMaxManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerView.transform);

        //味方デッキ
        _ownDeck = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownDeck"), _canvas.transform);

        //味方フィールド
        _ownField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownField"), _canvas.transform);

        //味方手札
        _ownHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownHands"), _canvas.transform);

        //味方情報
        _ownPlayerView = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownPlayerView"), _canvas.transform);
        _ownHPText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerView.transform);
        _ownManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerView.transform);
        _ownMaxManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerView.transform);

        _currentDrugParent = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/CurrentDrugParent"), _canvas.transform);
        _currentDrugParent.transform.parent.SetAsLastSibling();

        //ターン終了ボタン
        _turnEndButton = Instantiate(Resources.Load<Button>("UIPrefabs/Button"), _canvas.transform);
        _turnEndButton.transform.SetAsLastSibling();
        _turnEndButton.GetComponentInChildren<Text>().text = "ターン終了";
        _turnEndButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(700, 0, 0);

        //敵のターン終了ボタン（デバッグ用）
        _debugOpponentTurnEndButton = Instantiate(Resources.Load<Button>("UIPrefabs/Button"), _canvas.transform);
        _debugOpponentTurnEndButton.transform.SetAsLastSibling();
        _debugOpponentTurnEndButton.GetComponentInChildren<Text>().text = "敵ターン終了";
        _debugOpponentTurnEndButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-700, 0, 0);
    }


    /// <summary>
    /// IDに応じたカードをUI上に生成する
    /// </summary>
    /// <param name="cardID"></param>
    public void CreateHandsObject(UnitType unitType, int cardID)
    {
        var battleCardPrefab = Resources.Load<GameObject>($"CardPrefab/Battle/Card{cardID}");
        GameObject battleCard = null;
        switch (unitType)
        {
            case UnitType.Player:
                battleCard = Instantiate(battleCardPrefab, _ownHands.transform);
                battleCard.GetComponent<BattleCard>().OwnerType = UnitType.Player;
                return;
            case UnitType.Opponent:
                battleCard = Instantiate(battleCardPrefab, _opponentHands.transform);
                battleCard.GetComponent<BattleCard>().OwnerType = UnitType.Opponent;
                return;
        }  
        _cardObjectList.Add(battleCard.GetComponent<BattleCard>());
    }
}

