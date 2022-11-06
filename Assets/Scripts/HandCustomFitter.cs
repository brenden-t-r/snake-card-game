using UnityEngine;
using UnityEngine.UI;

/*
 * Duplicate of Hand class, but configured for the customer fitter hand.
 */
public class HandCustomFitter : MonoBehaviour
{
    private readonly int HAND_LAYER = 3;
    [SerializeField] private float SCALE_FACTOR = 0; // Hack for camera scale difference
    [SerializeField] private GameObject prefabCard;
    private int numOfCards = 0;

    public void Start()
    {
         Events.EventDrawCard.AddListener(DrawCard);
    }

    /*
     * Draw a specific card
     */
    private void DrawCard(CardScriptableObject type)
    {
        numOfCards += 1;
        GameObject card = Instantiate(prefabCard, transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.SetType(type);
        cardScript.Initialize();
        DoSetupCard(card);
    }

    /*
     * Draw a card (currently just draws a dummy card)
     * TODO: Integrate with Deck.
     */
    public void Draw()
    {
        numOfCards += 1;
        GameObject card = Instantiate(prefabCard, transform);
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
                cardTransform.localScale.y, 0
                );
        }
        Utils.SetLayerAllChildren(transform, HAND_LAYER);
    }
}
