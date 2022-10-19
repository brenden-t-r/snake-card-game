using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    private readonly float Z_OFFSET = 0.2f;
    private readonly float LAYOUT_MIN_WIDTH = 0.2f;
    private readonly float LAYOUT_PREFERRED_WIDTH = 2.8f;
    private readonly int HAND_LAYER = 3;
    [SerializeField] private GameObject prefabCard;
    private int numOfCards = 0;

    /*
     * Draw a card (currently just draws a dummy card)
     * TODO: Integrate with Deck.
     */
    public void Draw()
    {
        numOfCards += 1;
        GameObject card = Instantiate(prefabCard, transform);
        card.layer = HAND_LAYER;
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        LayoutElement layoutElement = card.AddComponent<LayoutElement>();
        layoutElement.minWidth = LAYOUT_MIN_WIDTH;
        layoutElement.preferredWidth = LAYOUT_PREFERRED_WIDTH;
        rectTransform.position = new Vector3(0, 0, numOfCards * Z_OFFSET);
        rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, 0);
        card.AddComponent<HandCard>();
        Utils.SetLayerAllChildren(transform, HAND_LAYER);
    }
}
