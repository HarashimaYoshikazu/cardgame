using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<InventryCard> _inventryCards = new List<InventryCard>();
    public List<InventryCard> InventryCards => _inventryCards;

    public List<InventryCard> _decksCards = new List<InventryCard>();
    public List <InventryCard> DeckCards => _decksCards;
}
