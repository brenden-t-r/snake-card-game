using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

/*
 * Duplicate of Hand class, but configured for the customer fitter hand.
 */
public class Hand : MonoBehaviour
{
    private readonly int HAND_LAYER = 3;
    [SerializeField] private float SCALE_FACTOR = 0; // Hack for camera scale difference
    [SerializeField] private GameObject prefabCard;
    [SerializeField] private GameObject prefabFood;
    private int numOfCards = 0;

    public void Start()
    {
        Events.EventDrawCard.AddListener(DrawCard);
        Events.EventDoHandUpdate.AddListener(HandUpdateEvent);
        Events.EventDiscardHand.AddListener(DiscardHand);
    }

    private void DrawCard(CardBase card)
    {
        switch (card.GetCardType())
        {
            case CardBase.CardType.SNAKE:
                DrawSnake((CardScriptableObject) card);
                break;
            case CardBase.CardType.FOOD:
                DrawFood((FoodCardScriptableObject) card);
                break;
            default:
                throw new InvalidEnumArgumentException();
        }
        HandUpdateEvent();
    }
    
    private void DrawSnake(CardScriptableObject type)
    {
        numOfCards += 1;
        GameObject card = Instantiate(prefabCard, transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.SetType(type);
        cardScript.Initialize();
        card.AddComponent<HandCardPlay>();
        DoSetupCard(card);
    }
    
    private void DrawFood(FoodCardScriptableObject type)
    {
        numOfCards += 1;
        GameObject card = Instantiate(prefabFood, transform);
        FoodCard cardScript = card.GetComponent<FoodCard>();
        cardScript.SetType(type);
        cardScript.Initialize();
        DoSetupCard(card);
    }

    private void DoSetupCard(GameObject card)
    {
        card.layer = HAND_LAYER;
        Transform cardTransform = card.GetComponent<Transform>();
        if (SCALE_FACTOR > 0)
        {
            cardTransform.localScale = new Vector3(
                cardTransform.localScale.x * SCALE_FACTOR, 
                cardTransform.localScale.y, cardTransform.localScale.z
                );
        }
        Utils.SetLayerAllChildren(transform, HAND_LAYER);
    }

    private void DiscardHand()
    {
        List<CardBase> cards = GetCardBaseFromChildren();
        Events.EventDiscard.Invoke(cards);
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        HandUpdateEvent();
        Debug.Log("Hand:DiscardHand");
    }

    private void HandUpdateEvent()
    {
        StartCoroutine(DoHandUpdateEventNextFrame());
    }
    
    private IEnumerator DoHandUpdateEventNextFrame()
    {
        // Wait a frame to accomodate any "Destroy()" calls made in the same frame as the event invocation.
        // Motivated by current implemented of cards with HandCardPlay script destroying themselves.
        yield return 0;
        Events.EventHandUpdate.Invoke(GetCardBaseFromChildren());
        yield return null;
    }
    
    private List<CardBase> GetCardBaseFromChildren()
    {
        List<CardBase> snakes = transform.GetComponentsInChildren<Card>().Select(x => (CardBase)x.GetCardType())
            .ToList();
        List<CardBase> food = transform.GetComponentsInChildren<FoodCard>().Select(x => (CardBase)x.GetType())
            .ToList();
        return snakes.Concat(food).ToList();
    }
}
