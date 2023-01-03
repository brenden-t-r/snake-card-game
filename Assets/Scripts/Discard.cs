using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    private List<CardBase> cards = new List<CardBase>();

    public void Start()
    {
        Events.EventDiscard.AddListener(DiscardCards);
        Events.ShuffleDiscard.AddListener(ShuffleAndReset);
    }

    private void DiscardCards(List<CardBase> cardBases)
    {
        Debug.Log("Discard:DiscardCards");
        cards.AddRange(cardBases);
        DiscardUpdateEvent();
    }

    private void ShuffleAndReset()
    {
        cards = Utils.ShuffleFisherYates(cards);
        Events.ShuffleDiscardCallback.Invoke(cards);
        cards = new List<CardBase>();
        DiscardUpdateEvent();
    }
    
    private void DiscardUpdateEvent()
    {
        Events.EventDiscardUpdate.Invoke(cards);
    }
}
