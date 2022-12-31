using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodCard : MonoBehaviour
{
    [SerializeField] private FoodCardScriptableObject cardScriptableObject;
    [SerializeField] private TMP_Text textTitle;
    [SerializeField] private Image image;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
         Initialize();
    }

    public void SetType(FoodCardScriptableObject type)
    {
        cardScriptableObject = type;
    }

    public FoodCardScriptableObject GetType()
    {
        return cardScriptableObject;
    }
    
    public void Initialize()
    {
        textTitle.text = cardScriptableObject.FoodTypeToString();
        if (image) {
            image.sprite = cardScriptableObject.sprite;
        }
        if (spriteRenderer)
        {
            spriteRenderer.sprite = cardScriptableObject.sprite;
        }
    }
}
