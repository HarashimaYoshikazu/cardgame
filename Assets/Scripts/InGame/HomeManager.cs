using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : Singleton<HomeManager>
{
    DeckCustomUIManager _deckCustomUIManager = null;
    public DeckCustomUIManager DeckCustomUIManager => _deckCustomUIManager;
    public void SetDeckCustomUIManager(DeckCustomUIManager deckCustomUIManager)
    {
        _deckCustomUIManager = deckCustomUIManager;
    }
}
