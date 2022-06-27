using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    List<InventryCard> _inventryCards = new List<InventryCard>();
    /// <summary>インベントリのカードリスト</summary>
    public List<InventryCard> InventryCards => _inventryCards;

    List<InventryCard> _decksCards = new List<InventryCard>();
    /// <summary>デッキのカードリスト</summary>
    public List <InventryCard> DeckCards => _decksCards;
}
