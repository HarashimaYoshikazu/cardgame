using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BattleCard : MonoBehaviour
{
    [SerializeField]
    int cardID;
    public int CardID => cardID;

    [SerializeField]
    Image _image;

    [SerializeField]
    Button _button;

    CardData _cardData;

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
    }

}
