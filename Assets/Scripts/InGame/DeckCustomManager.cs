using UnityEngine;

/// <summary>
/// デッキをUI上に表示するマネージャークラス
/// </summary>
public class DeckCustomManager : MonoBehaviour
{
    Canvas _canvas = null;
    GameObject _inventryPanel = null;
    GameObject _deckPanel = null;

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

        for (int i = 0; i < 20; i++)
        {
            CreateDeckCard(1);
        }
    }

    void CreateDeckCard(int id)
    {
        var go = Resources.Load<GameObject>($"CardPrefab/Card{id}");

        var card = Instantiate(go, _deckPanel.transform);
        GameManager.Instance.AddCardToDeck(card.GetComponent<InventryCard>());
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
            GameManager.Instance.RemoveCardToDeck(card);
            GameManager.Instance.InventryCards.Add(card);
            card.gameObject.transform.SetParent(_inventryPanel.transform);
        }
        else
        {
            GameManager.Instance.InventryCards.Remove(card);
            GameManager.Instance.AddCardToDeck(card);
            card.gameObject.transform.SetParent(_deckPanel.transform);
        }
        card.SetIsDeck(!isDeck);

    }
}
