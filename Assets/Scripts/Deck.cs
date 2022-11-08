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
    }

    public List<GameObject> GetCards()
    {
        return cards;
    }

    public void DrawCard()
    {
        // Remove top card from deck
        // TODO: Animation
        GameObject card = cards[cards.Count-1];
        cards = cards.Take(cards.Count - 1).ToList();
        Destroy(card);

        // Trigger draw card event (subscribed by Hand)
        CardScriptableObject type = card.GetComponent<Card>().GetType();
        Events.EventDrawCard.Invoke(type);
    }
}
