using System.ComponentModel;
using UnityEngine;

/*
 * Duplicate of Hand class, but configured for the customer fitter hand.
 */
public class HandCustomFitter : MonoBehaviour
{
    private readonly int HAND_LAYER = 3;
    [SerializeField] private float SCALE_FACTOR = 0; // Hack for camera scale difference
    [SerializeField] private GameObject prefabCard;
    [SerializeField] private GameObject prefabFood;
    private int numOfCards = 0;

    public void Start()
    {
         Events.EventDrawCard.AddListener(DrawCard);
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
    }

    /*
     * Draw a specific card
     */
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
}
