using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BattleCard : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("カードの固有番号")]
    int cardID;
    /// <summary>カードの固有番号</summary>
    public int CardID => cardID;

    /// <summary>カード背景のImageクラス</summary>
    Image _backGroundImage = null;

    [SerializeField, Tooltip("カード自体のImageクラス")]
    Image _cardImage = null;

    [SerializeField, Tooltip("カードの名前テキストクラス")]
    Text _nameText = null;

    [SerializeField, Tooltip("コストを表示するテキストクラス")]
    Text _costText = null;

    [SerializeField, Tooltip("攻撃力を表示するテキストクラス")]
    Text _attackText = null;

    [SerializeField, Tooltip("HPを表示するテキストクラス")]
    Text _hpText = null;

    [SerializeField, Tooltip("技を選択するパネル")]
    SkillPanel _skillPanel = null;

    /// <summary>カードのRectTransformクラス</summary>
    RectTransform _rectTransform;

    /// <summary>カードの情報を格納したクラスのインスタンス</summary>
    CardData _cardData;

    BattleCardState _cardState = BattleCardState.InHand;
    public void ChangeCardState(BattleCardState battleCardState)
    {
        _cardState = battleCardState;
    }

    UnitType _owner;
    /// <summary>このカードの所有者</summary>
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
        _cardData = new CardData(cardID, _attackText, _hpText, _costText, this.gameObject);

        //コンポーネントのキャッシュ
        if (!_backGroundImage)
        {
            _backGroundImage = GetComponent<Image>();
        }

        if (!_cardImage)
        {
            _cardImage = Instantiate(Resources.Load<Image>("UIPrefabs/ImageObject"), this.transform);
            _cardImage.rectTransform.anchoredPosition = new Vector2(0, 40);
            _cardImage.raycastTarget = false;
        }
        _cardImage.sprite = _cardData.Sprite;

        _nameText.text = _cardData.Name;

        if (!_rectTransform)
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        _skillPanel.SetSkillValue(_cardData.SkillValue);
    }

    public void OnBeginDrag()
    {
        switch (_cardState)
        {
            case BattleCardState.InField:

                break;
            case BattleCardState.InHand:
                //カードの下のObjectを取得したいからraycastTargetを無効にする
                _backGroundImage.raycastTarget = false;
                //ドラッグしてるとき用のオブジェクトの子オブジェクトにする
                this.transform.SetParent(BattleManager.Instance.CurrentDrugParent.transform);
                break;
        }
    }

    public void OnDrag(Vector2 pos)
    {
        switch (_cardState)
        {
            case BattleCardState.InField:
                break;
            case BattleCardState.InHand:
                //ドラッグ中はポインターに追従
                _rectTransform.position = pos;
                break;
        }
    }

    public void OnPointerUp(GameObject target)
    {
        switch (_cardState)
        {
            case BattleCardState.InField:
                Attack(target);
                break;
            case BattleCardState.InHand:
                //自分のフィールドオブジェクトだったら子オブジェクトにする
                if (target == BattleManager.Instance.OwnFields && _cardData.Cost <= BattleManager.Instance.Player.CurrentMana)
                {
                    PlayCard(BattleManager.Instance.Player);
                }
                else
                {
                    this.transform.SetParent(BattleManager.Instance.OwnHandsUI.transform);
                }

                break;
        }
        _backGroundImage.raycastTarget = true;
    }

    private void PlayCard(UnitData unit)
    {
        unit.ChangeCurrentMana(-(_cardData.Cost));
        BattleManager.Instance.PlayCard(unit.Type,this);
    }

    public void Attack(IDamage targetDamage)
    {
        targetDamage.Damage(-_cardData.Attack);
        Debug.Log($"{targetDamage}に{_cardData.Attack}ダメージ");
    }

    public void Attack(GameObject target)
    {
        if (target && target.TryGetComponent(out IDamage damage))
        {
            Attack(damage);
        }
    }


    public void Damage(int value)
    {
        if (_cardState == BattleCardState.InField)
        {
            _cardData.ChangeHP(value);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _skillPanel.gameObject.SetActive(!_skillPanel.gameObject.activeSelf);
    }
}
public enum BattleCardState
{
    InHand,
    InField
}