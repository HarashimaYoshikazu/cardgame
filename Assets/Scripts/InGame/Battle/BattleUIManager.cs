using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// UI上のオブジェクトを生成、管理するクラス
/// </summary>
public class BattleUIManager : MonoBehaviour
{
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

    /// <summary>手札のカードUIの配列 </summary>
    List<BattleCard> _opponentHandCards = new List<BattleCard>();
    public BattleCard[] OpponentHandCards => _opponentHandCards.ToArray();
    /// <summary>場のカードUIの配列 </summary>
    List<BattleCard> _opponentFieldCards = new List<BattleCard>();
    public BattleCard[] OpponentFieldCards => _opponentFieldCards.ToArray();

    GameObject _opponentDeck = null;
    public GameObject OpponentDeck => _opponentDeck;

    GameObject _opponentField = null;
    public GameObject OpponentField => _opponentField;

    GameObject _opponentHands = null;
    public GameObject OpponentHands => _opponentHands;

    GameObject _opponentPlayerDataPanel = null;
    public GameObject OpponentPlayerDataPanel => _opponentPlayerDataPanel;
    UnitView _opponentPlayerView = null;
    public UnitView OpponentPlayerView => _opponentPlayerView;
    Text _opponentHPText = null;
    public Text OpponentHPText => _opponentHPText;
    Text _opponentCurrentManaText = null;
    public Text OpponentCurrentManaText => _opponentCurrentManaText;
    Text _opponentMaxManaText = null;
    public Text OpponentMaxManaText => _opponentMaxManaText;

    /************
     自分側
      ***********/
    /// <summary>手札のカードUIの配列 </summary>
    List<BattleCard> _ownHandCards = new List<BattleCard>();
    public BattleCard[] OwnHandCards => _ownHandCards.ToArray();
    /// <summary>場のカードUIの配列 </summary>
    List<BattleCard> _ownFieldCards = new List<BattleCard>();
    public BattleCard[] OwnFieldCards => _ownFieldCards.ToArray();

    GameObject _ownDeck = null;
    public GameObject Oendeck => _ownDeck;

    GameObject _ownField = null;
    public GameObject OwnField => _ownField;

    GameObject _ownHands = null;
    public GameObject OwnHands => _ownHands;

    GameObject _ownPlayerDataPanel = null;
    public GameObject OwnPlayerDataPanel => _ownPlayerDataPanel;
        UnitView _ownPlayerView = null;
    public UnitView OwnPlayerView => _opponentPlayerView;
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
        _opponentPlayerDataPanel = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/opponentPlayerView"), _canvas.transform);
        _opponentPlayerView = UnitView.Instantiate(Resources.Load<UnitView>("UIPrefabs/Battle/UnitObject"), _opponentPlayerDataPanel.transform,UnitType.Opponent);
        _opponentHPText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerView.transform);
        _opponentCurrentManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerDataPanel.transform);
        _opponentMaxManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _opponentPlayerDataPanel.transform);

        //味方デッキ
        _ownDeck = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownDeck"), _canvas.transform);

        //味方フィールド
        _ownField = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownField"), _canvas.transform);

        //味方手札
        _ownHands = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownHands"), _canvas.transform);

        //味方情報
        _ownPlayerDataPanel = Instantiate(Resources.Load<GameObject>("UIPrefabs/Battle/ownPlayerView"), _canvas.transform);
        _ownPlayerView = UnitView.Instantiate(Resources.Load<UnitView>("UIPrefabs/Battle/UnitObject"), _ownPlayerDataPanel.transform, UnitType.Player);
        _ownHPText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerView.transform);
        _ownManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerDataPanel.transform);
        _ownMaxManaText = Instantiate(Resources.Load<Text>("UIPrefabs/Battle/TextObject"), _ownPlayerDataPanel.transform);

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

    BattleCard ConvertCard(BattleCard[] battleCards, int cardID)
    {
        var cards = battleCards.Where(c => c.CardID == cardID);
        if (cards.Count() <= 0)
        {
            Debug.LogError($"パラメータの配列に指定のカードが含まれていません。配列：{battleCards}cardID:{cardID}");
        }
        return cards.Single();
    }

    /// <summary>
    /// IDに応じたカードをUI上に生成する
    /// </summary>
    /// <param name="cardID"></param>
    public void CreateHandsObject(UnitType unitType, int cardID)
    {
        var battleCardPrefab = Resources.Load<BattleCard>($"CardPrefab/Battle/Card{cardID}");
        BattleCard battleCard = null;
        switch (unitType)
        {
            case UnitType.Player:
                battleCard = Instantiate(battleCardPrefab, _ownHands.transform);
                AddHand(UnitType.Player,battleCard);
                battleCard.OwnerType = UnitType.Player; //今は直接代入してる
                break;
            case UnitType.Opponent:
                battleCard = Instantiate(battleCardPrefab, _opponentHands.transform);
                AddHand(UnitType.Opponent, battleCard);
                battleCard.OwnerType = UnitType.Opponent;
                break;
        }  
    }

    public void AddHand(UnitType unitType,BattleCard card)
    {
        switch (unitType)
        {
            case UnitType.Player:
                _ownHandCards.Add(card);
                card.transform.SetParent(_ownHands.transform);
                break;
            case UnitType.Opponent:
                _opponentHandCards.Add(card);
                card.transform.SetParent(_opponentHands.transform);
                break;
        }
    }

    public void RemoveHand(UnitType unitType, BattleCard card)
    {
        switch (unitType)
        {
            case UnitType.Player:
                _ownHandCards.Remove(card);
                break;
            case UnitType.Opponent:
                _opponentHandCards.Remove(card);
                break;
        }
    }
    public void RemoveHand(UnitType unitType, BattleCard[] battleCards, int cardID)
    {
        var card = ConvertCard(battleCards, cardID);
        RemoveHand(unitType, card);
    }

    public void AddField(UnitType unitType, BattleCard card)
    {
        switch (unitType)
        {
            case UnitType.Player:
                _ownFieldCards.Add(card);
                card.transform.SetParent(_ownField.transform);
                break;
            case UnitType.Opponent:
                _opponentFieldCards.Add(card);
                card.transform.SetParent(_opponentField.transform);
                break;
        }
    }

    public void AddField(UnitType unitType, BattleCard[] battleCards, int cardID)
    {
        var card = ConvertCard(battleCards,cardID);
        AddField(unitType,card);
    }

    public void RemoveField(UnitType unitType, BattleCard card)
    {
        switch (unitType)
        {
            case UnitType.Player:
                _ownFieldCards.Remove(card);
                break;
            case UnitType.Opponent:
                _opponentFieldCards.Remove(card);
                break;
        }
    }
}

