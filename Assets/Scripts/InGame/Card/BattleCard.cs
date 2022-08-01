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

    /// <summary>カード背景のImageクラス</summary>
    Image _backGroundImage = null;

    [SerializeField, Tooltip("カード自体のImageクラス")]
    Image _cardImage = null;

    [SerializeField,Tooltip("カードの名前テキストクラス")]
    Text _nameText = null;

    [SerializeField,Tooltip("コストを表示するテキストクラス")]
    Text _costText = null;

    [SerializeField,Tooltip("攻撃力を表示するテキストクラス")]
    Text _attackText = null;

    [SerializeField,Tooltip("HPを表示するテキストクラス")]
    Text _hpText = null;

    /// <summary>カードのRectTransformクラス</summary>
    RectTransform _rectTransform;

    /// <summary>カードの情報を格納したクラスのインスタンス</summary>
    CardData _cardData;

    /// <summary>キャッシュ用の変数</summary>
    GameObject _currentPointerObject = null;

    UnitType _owner;
    public UnitType OwnerType
    {
        get { return _owner; }
        set { _owner = value; }
    }

    private void Awake()
    {
        Init();
    }
    /// <summary>
    /// カードの初期化関数
    /// </summary>
    void Init()
    {
        //カードデータをIDに基づいて生成
        _cardData = new CardData(cardID,_attackText,_hpText,_costText,this.gameObject);

        //コンポーネントのキャッシュ
        if (!_backGroundImage)
        {
            _backGroundImage = GetComponent<Image>();
        }

        if (!_cardImage)
        {
            _cardImage = Instantiate(Resources.Load<Image>("UIPrefabs/ImageObject"),this.transform);
            _cardImage.rectTransform.anchoredPosition = new Vector2(0,40);
            _cardImage.raycastTarget = false;
        }
        _cardImage.sprite = _cardData.Sprite;

        _nameText.text = _cardData.Name;

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
        //今はオーナーをPlayerで固定してる
        if (!BattleManager.Instance.IsMyTurn || _owner != UnitType.Player)
        {
            return;
        }
        //カードの下のObjectを取得したいからraycastTargetを無効にする
        _backGroundImage.raycastTarget = false;
        //ドラッグしてるとき用のオブジェクトの子オブジェクトにする
        this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.CurrentDrugParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn || _owner != UnitType.Player)
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
        if (!BattleManager.Instance.IsMyTurn || _owner != UnitType.Player)
        {
            return;
        }
        //自分のフィールドオブジェクトだったら子オブジェクトにする
        if (_currentPointerObject == BattleManager.Instance.BattleUIManagerInstance.OwnField && _cardData.Cost <= BattleManager.Instance.Player.CurrentMana)
        {
            BattleManager.Instance.Player.ChangeCurrentMana(-(_cardData.Cost));
            this.transform.SetParent(_currentPointerObject.transform);
        }
        //違ったらraycastTargetを有効にして手札に戻す
        else
        {
            _backGroundImage.raycastTarget = true;
            this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.OwnHands.transform);
        }

    }
}