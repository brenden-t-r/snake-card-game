using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    static public GameData instance;

    public float playerHealth;
    public List<CardBase> playerHand = new List<CardBase>();
    public List<CardBase> playerDeck;
    public List<CardBase> playerDiscard;
    public List<CardScriptableObject> playerCardsInPlay;

    private float opponentHealth;
    private List<CardBase> opponentHand;
    private List<CardBase> opponentDeck;
    private List<CardBase> opponentDiscard;
    private List<CardScriptableObject> opponentCardsInPlay;

    private void Start()
    {
        instance = this;
        Events.EventHandUpdate.AddListener(cards => playerHand = cards);
        Events.EventDeckUpdate.AddListener(cards => playerDeck = cards);
        Events.EventDiscardUpdate.AddListener(cards => playerDiscard = cards);
        Events.EventHealthUpdate.AddListener(health => playerHealth = health);
    }

    // private void Update()
    // {
    //     Debug.Log("Player cards in hand: " + playerHand.Count);
    // }
}
