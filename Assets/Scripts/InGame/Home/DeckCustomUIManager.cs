using UnityEngine;

/// <summary>
/// デッキをUI上に表示するマネージャークラス
/// </summary>
public class DeckCustomUIManager : MonoBehaviour
{
    Canvas _canvas = null;
    GameObject _inventryPanel = null;
    GameObject _deckPanel = null;

    //生成だけしてAwakeでやる方が賢いかも
    /// <summary>
    /// UIオブジェクトを動的に生成する関数
    /// </summary>
    public void SetUpUIObject()
    {
        _canvas = FindObjectOfType<Canvas>();
        if (!_canvas)
        {
            _canvas = Instantiate(Resources.Load<Canvas>("UIPrefabs/Canvas")); ;
        }
        _deckPanel = Instantiate(Resources.Load<GameObject>("UIPrefabs/Decks"), _canvas.transform);
        _inventryPanel = Instantiate(Resources.Load<GameObject>("UIPrefabs/Inventry"), _canvas.transform); ;

        Instantiate(Resources.Load<GameObject>("UIPrefabs/ButtonCanvas"));


        //とりあえず最初に20枚追加
        for (int i = 0; i < 20; i++)
        {
            CreateDeckCard(1);
        }
    }

    /// <summary>
    /// デッキリストに新しくカードを追加する関数
    /// </summary>
    /// <param name="id">追加したいカードのID</param>
    void CreateDeckCard(int id)
    {
        var goPrefab = Resources.Load<GameObject>($"CardPrefab/Card{id}");

        var go = Instantiate(goPrefab, _deckPanel.transform);
        var card = go.GetComponent<InventryCard>();
        card.SetIsDeck(true);
        GameManager.Instance.AddCardToDeck(card.CardID);
    }

    /// <summary>
    /// カードをデッキ若しくはインベントリにセットする関数
    /// </summary>
    /// <param name="card">セットするカード</param>
    /// <param name="isDeck">現在の状態</param>
    public void SetCard(InventryCard card, bool isDeck)
    {
        if (isDeck)
        {
            GameManager.Instance.RemoveCardToDeck(card.CardID);
            GameManager.Instance.AddCardToInventry(card.CardID);
            card.gameObject.transform.SetParent(_inventryPanel.transform);
        }
        else
        {
            GameManager.Instance.RemoveCardToInventry(card.CardID);
            GameManager.Instance.AddCardToDeck(card.CardID);
            card.gameObject.transform.SetParent(_deckPanel.transform);
        }
        //card.SetIsDeck(!isDeck);

    }
}
