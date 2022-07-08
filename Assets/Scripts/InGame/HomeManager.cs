using UnityEngine;

/// <summary>
/// ホーム画面を管理するクラス
/// </summary>
public class HomeManager : Singleton<HomeManager>
{
    DeckCustomUIManager _deckCustomUIManager = null;

    public DeckCustomUIManager DeckCustomUIManager
    {
        get
        {
            if (!_deckCustomUIManager)
            {
                GameObject go = new GameObject();
                go.name = "DeckCustomUIManager";
                var deckCustom = go.AddComponent<DeckCustomUIManager>();
                _deckCustomUIManager = deckCustom;
            }
            return _deckCustomUIManager;
        }
        set { _deckCustomUIManager = value; }
    }
}
