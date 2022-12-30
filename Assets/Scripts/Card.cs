using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private CardScriptableObject cardScriptableObject;
    [SerializeField] private Renderer renderer;
    [SerializeField] private TMP_Text textTitle;
    [SerializeField] private TMP_Text textAttack;
    [SerializeField] private TMP_Text textHealth;
    [SerializeField] private TMP_Text textFoodCost;
    [SerializeField] private SpriteRenderer spriteAmphibian;
    [SerializeField] private SpriteRenderer spriteReptile;
    [SerializeField] private SpriteRenderer spriteSmallMammal;
    [SerializeField] private SpriteRenderer spriteLargeMammal;
    private float ICON_LOW_OPACITY = 0.33f;

    public void Start()
    {
        Initialize();
    }

    public void SetType(CardScriptableObject type)
    {
        cardScriptableObject = type;
    }

    public CardScriptableObject GetCardType()
    {
        return cardScriptableObject;
    }

    public void Initialize()
    {
        if (renderer is SpriteRenderer)
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
            spriteRenderer.sprite = cardScriptableObject.sprite;
        }
        textTitle.SetText(cardScriptableObject.title);
        textAttack.SetText(cardScriptableObject.attack.ToString());
        textHealth.SetText(cardScriptableObject.health.ToString());
        textFoodCost.SetText(cardScriptableObject.foodRequirement.ToString());
        spriteAmphibian.color = new Color(0, 0, 0, cardScriptableObject.foodAmphibian ? 1 : ICON_LOW_OPACITY);
        spriteReptile.color = new Color(0, 0, 0, cardScriptableObject.foodReptile ? 1 : ICON_LOW_OPACITY);
        spriteSmallMammal.color = new Color(0, 0, 0, cardScriptableObject.foodSmallMammal ? 1 : ICON_LOW_OPACITY);
        spriteLargeMammal.color = new Color(0, 0, 0, cardScriptableObject.foodLargeMammal ? 1 : ICON_LOW_OPACITY);
    }
    
}
