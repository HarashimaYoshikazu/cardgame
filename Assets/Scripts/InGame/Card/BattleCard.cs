using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BattleCard : MonoBehaviour,IDragHandler,IPointerUpHandler,IBeginDragHandler
{
    [SerializeField]
    int cardID;
    public int CardID => cardID;

    [SerializeField]
    Image _image;

    [SerializeField]
    Button _button;

    [SerializeField]
    RectTransform _rectTransform;

    CardData _cardData;

    BattleCardState _currentState = BattleCardState.Hands;

    GameObject _currentPointerObject = null;
    Transform _handsObject = null;

    private void Start()
    {
        Init();

        if (!_image)
        {
            _image = GetComponent<Image>();
        }
        _image.sprite = _cardData.Sprite;

        if (!_button)
        {
            _button = GetComponent<Button>();
        }
        _button.onClick.AddListener(() =>
        {

        });
        
    }
    void Init()
    {
        _cardData = new CardData(cardID);
        if (!_rectTransform)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        _handsObject = BattleManager.Instance.BattleUIManagerInstance.OwnHands.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _currentPointerObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(_currentPointerObject.name);
        _rectTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_currentPointerObject.CompareTag("OwnField"))
        {
            this.transform.SetParent(_currentPointerObject.transform);
        }
        else
        {
            _image.raycastTarget = true;
            this.transform.SetParent(_handsObject);
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        this.transform.SetAsLastSibling();
        this.transform.parent = BattleManager.Instance.BattleUIManagerInstance.CurrentDrugParent.transform;
    }
}

public enum BattleCardState
{
    Hands,
    FIeld,
    Trash
}
