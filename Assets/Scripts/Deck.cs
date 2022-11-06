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
        // Create prefab for each card in cardTypes
        foreach (CardScriptableObject type in cardTypes)
        {
            GameObject card = Instantiate(prefabCard, transform);
            card.transform.localRotation = Quaternion.Euler(0, -180f, 0);
            Card cardScript = card.GetComponent<Card>();
            cardScript.SetType(type);
            cardScript.Initialize();
            cards.Add(card);
        }
    }

    public List<GameObject> GetCards()
    {
        return cards;
    }

    public void DrawCard()
    {
        // Remove top card from deck
        // TODO: Animation
        GameObject card = cards[0];
        cards = cards.Skip(1).ToList();
        Destroy(card);

        // Trigger draw card event (subscribed by Hand)
        CardScriptableObject type = card.GetComponent<Card>().GetType();
        Events.EventDrawCard.Invoke(type);
    }
}
