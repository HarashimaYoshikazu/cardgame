using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BattleCard : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler
{
    [SerializeField, Tooltip("カードの固有番号")]
    int cardID;
    /// <summary>カードの固有番号</summary>
    public int CardID => cardID;

    /// <summary>カードのImageクラス</summary>
    Image _image;

    /// <summary>カードのButtonクラス</summary>
    Button _button;

    /// <summary>カードのRectTransformクラス</summary>
    RectTransform _rectTransform;

    /// <summary>カードの情報を格納したクラスのインスタンス</summary>
    CardData _cardData;

    BattleCardState _currentState = BattleCardState.Hands;
    public void SetCurrentCardState(BattleCardState battleCardState)
    {
        switch (battleCardState)
        {
            case BattleCardState.Hands:
                break;
            case BattleCardState.FIeld:
                break;
        }

        _currentState = battleCardState;
    }

    /// <summary>キャッシュ用の変数</summary>
    GameObject _currentPointerObject = null;

    private void Awake()
    {
        Init();
        _button.onClick.AddListener(() =>
        {

        });

    }
    /// <summary>
    /// カードの初期化関数
    /// </summary>
    void Init()
    {
        //カードデータをIDに基づいて生成
        _cardData = new CardData(cardID);

        //コンポーネントのキャッシュ
        if (!_image)
        {
            _image = GetComponent<Image>();
        }
        _image.sprite = _cardData.Sprite;

        if (!_button)
        {
            _button = GetComponent<Button>();
        }

        if (!_rectTransform)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        _currentPointerObject = BattleManager.Instance.BattleUIManagerInstance.CurrentPointerObject;
    }

    /*
    以下EventSystemsのインターフェイスの関数
    */
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn)
        {
            return;
        }
        //カードの下のObjectを取得したいからraycastTargetを無効にする
        _image.raycastTarget = false;
        //ドラッグしてるとき用のオブジェクトの子オブジェクトにする
        this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.CurrentDrugParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn)
        {
            return;
        }
        //現在ポインター上にあるオブジェクトを検知して代入
        _currentPointerObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(_currentPointerObject.name);
        //ドラッグ中はポインターに追従
        _rectTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn)
        {
            return;
        }
        //自分のフィールドオブジェクトだったら子オブジェクトにする
        if (_currentPointerObject == BattleManager.Instance.BattleUIManagerInstance.OwnField && _cardData.Cost <= BattleManager.Instance.Player.CurrentMana)
        {
            BattleManager.Instance.Player.ChangeMana(-(_cardData.Cost));
            this.transform.SetParent(_currentPointerObject.transform);
        }
        //違ったらraycastTargetを有効にして手札に戻す
        else
        {
            _image.raycastTarget = true;
            this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.OwnHands.transform);
        }

    }
}

public enum BattleCardState
{
    Hands,
    FIeld,
}
