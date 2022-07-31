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

    [SerializeField, Tooltip("コストを表示するテキストクラス")]
    Text _costText = null;

    [SerializeField, Tooltip("攻撃力を表示するテキストクラス")]
    Text _attackText = null;

    [SerializeField, Tooltip("HPを表示するテキストクラス")]
    Text _hpText = null;

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

        _button.onClick.AddListener
            (() =>
            {
                HomeManager.Instance.DeckCustomUIManager.SetCard(this, _isDeck);
                _isDeck = !_isDeck;
            });

    }

    public void SetIsDeck(bool isdeck)
    {
        _isDeck = isdeck;
    }

    void Init()
    {
        _cardData = new CardData(cardID,_attackText,_hpText,_costText, this.gameObject);
    }
}
