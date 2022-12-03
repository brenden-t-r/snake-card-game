using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private CardScriptableObject cardScriptableObject;
    [SerializeField] private Renderer renderer;
    [SerializeField] private TMP_Text textTitle;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private TMP_Text textFunFact;
    [SerializeField] private TMP_Text textEcosystem;
    [SerializeField] private TMP_Text textMechanics;

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
        renderer.material = cardScriptableObject.material;
        if (renderer is SpriteRenderer)
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
            spriteRenderer.sprite = cardScriptableObject.sprite;
        }
        textTitle.SetText(cardScriptableObject.title);
        //textDescription.SetText(cardScriptableObject.description);
        //textFunFact.SetText(cardScriptableObject.funFact);
        //textEcosystem.SetText(cardScriptableObject.ecosystem.ToString());
       // List<string> mechanics = cardScriptableObject.mechanics
       //     .Select(x => x.ToString()).ToList();
       // textMechanics.SetText(String.Join(", ", mechanics));
    }
    
}
