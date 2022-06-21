using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class InventryCard : MonoBehaviour
{
    [SerializeField]
    int cardID;
    public int CardID => cardID;

    [SerializeField]
    Image _image;

    [SerializeField]
    Button _button;

    CardData _cardData;

    bool _isDeck = false;
    private void Start()
    {
        Init();

        if (!_image)
        {
            _image = GetComponent<Image>();
        }

        if (!_button)
        {
            _button = GetComponent<Button>();
        }

        _image.sprite = _cardData.Sprite;

        _button.onClick.AddListener(() => GameManager.Instance.SetCard(this,_isDeck));
        _button.onClick.AddListener(() => _isDeck = !_isDeck);
    }

    void Init()
    {
        _cardData = new CardData(cardID);
    }
}
