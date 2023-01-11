using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardScriptableObject> cardTypes;
    private List<CardBase> cards = new List<CardBase>();

    private void Start()
    {
        Events.EventDrawCardsFromDeck.AddListener(DrawCards);
        Events.ShuffleDiscardCallback.AddListener(ShuffleDiscardIntoDeck);

        int i = 0;
        foreach (CardScriptableObject type in cardTypes)
        {
            cards.Add(type);
            i += 1;
        }
        
        DeckUpdateEvent();
    }

    private void DrawCards(int x)
    {
        for (int i = 0; i < x; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (cards.Count == 0)
        {
            Events.ShuffleDiscard.Invoke();
        }
        
        CardBase card = cards[cards.Count-1];
        cards = cards.Take(cards.Count - 1).ToList();

        // Trigger draw card event (subscribed by Hand)
        Events.EventDrawCard.Invoke(card);
        
        DeckUpdateEvent();
    }

    private void ShuffleDiscardIntoDeck(List<CardBase> discard)
    {
        cards.AddRange(discard);
        DeckUpdateEvent();
    }
    
    private void DeckUpdateEvent()
    {
        Events.EventDeckUpdate.Invoke(cards);
    }
}
