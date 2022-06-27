using System.Collections.Generic;

/// <summary>
/// ゲーム全体を通して保存したい情報を制御するクラス
/// </summary>
public class GameManager : Singleton<GameManager>
{
    List<InventryCard> _inventryCards = new List<InventryCard>();
    /// <summary>インベントリのカードリスト</summary>
    public List<InventryCard> InventryCards => _inventryCards;

    List<InventryCard> _decksCards = new List<InventryCard>();
    /// <summary>デッキのカードリスト</summary>
    public List <InventryCard> DeckCards => _decksCards;
}
