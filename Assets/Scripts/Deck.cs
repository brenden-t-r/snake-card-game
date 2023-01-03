using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardScriptableObject> cardTypes;
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private GameObject prefabCard;

    private void Start()
    {
        Events.ShuffleDiscardCallback.AddListener(ShuffleDiscardIntoDeck);
        Events.EventDrawCardsFromHand.AddListener(DrawCards);
        
        // Create prefab for each card in cardTypes
        int i = 0;
        foreach (CardScriptableObject type in cardTypes)
        {
            GameObject card = Instantiate(prefabCard, transform);
            card.transform.localRotation = Quaternion.Euler(0, -180f, 0);
            card.transform.localPosition = new Vector3(0, i * 0.01f, i * -0.05f);
            Card cardScript = card.GetComponent<Card>();
            cardScript.SetType(type);
            cardScript.Initialize();
            cards.Add(card);
            i += 1;
        }
        
        DeckUpdateEvent();
    }

    public List<GameObject> GetCards()
    {
        return cards;
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
        GameObject card = cards[cards.Count-1];
        cards = cards.Take(cards.Count - 1).ToList();
        Destroy(card);

        // Trigger draw card event (subscribed by Hand)
        CardBase type = card.GetComponent<Card>().GetCardType();
        Events.EventDrawCard.Invoke(type);
    }

    public void ShuffleDiscardIntoDeck(List<GameObject> discard)
    {
        cards.AddRange(discard);
    }
    
    private void DeckUpdateEvent()
    {
        List<CardBase> cardsList = cards.Select(card =>
        {
            if (card.TryGetComponent(out Card cardComponent))
            {
                return cardComponent.GetCardType();
            }
            else
            {
                card.TryGetComponent(out FoodCard foodComponent);
                return (CardBase)foodComponent.GetType();
            }
        }).ToList();
        Events.EventDeckUpdate.Invoke(cardsList);
    }
}
