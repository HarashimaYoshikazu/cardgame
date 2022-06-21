using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //‚Ð‚Æ‚Ü‚¸UIŽü‚è‚ðGameManager‚É‚â‚ç‚¹‚Ä‚é
    //ToDo UIManager“I‚È“z‚Â‚­‚é
    Canvas _canvas = null;
    GameObject[] _panels = new GameObject[2];

    List<InventryCard> _inventryCards = new List<InventryCard>();
    List<InventryCard> _decksCards = new List<InventryCard>();

    public void SetUp()
    {
        UISetUp();
    }

    void UISetUp()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
        _panels[0] = GameObject.Instantiate(Resources.Load<GameObject>("UIPrefabs/Decks"), _canvas.transform); 
        _panels[1] = GameObject.Instantiate(Resources.Load<GameObject>("UIPrefabs/Inventry"), _canvas.transform); ;

        for (int i = 0;i<10;i++)
        {
            CreateInventryCard(1);
        }
    }

    void CreateInventryCard(int id)
    {
        var go = Resources.Load<GameObject>($"CardPrefab/Card{id}");
        Debug.Log(go);
        Debug.Log(_panels[1]);
        var card = GameObject.Instantiate(go, _panels[1].transform);
        _inventryCards.Add(card.GetComponent<InventryCard>()) ;
    }

    public void SetCard(InventryCard card,bool isDeck)
    {
        if (isDeck)
        {
            _decksCards.Remove(card);
            _inventryCards.Add(card);
            card.gameObject.transform.SetParent(_panels[1].transform);
        }
        else
        {
            _inventryCards.Remove(card);
            _decksCards.Add(card);
            card.gameObject.transform.SetParent(_panels[0].transform);
        }
        
    }
}
