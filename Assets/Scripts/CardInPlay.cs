using TMPro;
using UnityEngine;

public class CardInPlay : MonoBehaviour
{
    [SerializeField] private CardScriptableObject cardScriptableObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text textAttack;
    [SerializeField] private TMP_Text textHealth;
    
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
        spriteRenderer.sprite = cardScriptableObject.spriteInPlay;
        textAttack.SetText(cardScriptableObject.attack.ToString());
        textHealth.SetText(cardScriptableObject.health.ToString());
    }
}
