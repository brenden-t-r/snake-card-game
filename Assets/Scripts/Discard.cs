using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    // [SerializeField] private GameObject prefabCard;

    public void Start()
    {
        Events.ShuffleDiscard.AddListener(Shuffle);
    }

    private void AddCard(GameObject card)
    {
        cards.Add(card);
    }

    private void Shuffle()
    {
        cards = Utils.ShuffleFisherYates(cards);
        Events.ShuffleDiscardCallback.Invoke(cards);
        cards = new List<GameObject>();
    }
}
