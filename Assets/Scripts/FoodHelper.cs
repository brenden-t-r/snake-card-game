using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodHelper : MonoBehaviour
{
    [SerializeField] private CardScriptableObject cardScriptableObject;
    [SerializeField] private GameObject helperContainer;
    [SerializeField] private Image iconAmphibian;
    [SerializeField] private Image iconReptile;
    [SerializeField] private Image iconSmallMammal;
    [SerializeField] private Image iconLargeMammal;
    [SerializeField] private Image iconCheck;
    [SerializeField] private Image iconX;
    [SerializeField] private TMP_Text textHaveAmphibian;
    [SerializeField] private TMP_Text textHaveReptile;
    [SerializeField] private TMP_Text textHaveSmallMammal;
    [SerializeField] private TMP_Text textHaveLargeMammal;
    [SerializeField] private TMP_Text textHaveTotal;
    [SerializeField] private TMP_Text textCostTotal;
    private float ICON_LOW_OPACITY = 0.33f; // TODO Make shared constant

    public void SetType(CardScriptableObject type)
    {
        cardScriptableObject = type;
    }
    
    private void OnMouseEnter()
    {
        Initialize();
    }

    private void OnMouseExit()
    {
        Close();
    }

    public void Initialize()
    {
        // Lower opacity of non-foods
        // Set total cost
        // Set have amounts (how to get these from hand?)
        // Set check/X icon
        // Set overall opacity to non-zero
        iconAmphibian.color = new Color(255, 255, 255, cardScriptableObject.foodAmphibian ? 1 : ICON_LOW_OPACITY); // DRY with card?
        iconReptile.color = new Color(255, 255, 255, cardScriptableObject.foodReptile ? 1 : ICON_LOW_OPACITY);
        iconSmallMammal.color = new Color(255, 255, 255, cardScriptableObject.foodSmallMammal ? 1 : ICON_LOW_OPACITY);
        iconLargeMammal.color = new Color(255, 255, 255, cardScriptableObject.foodLargeMammal ? 1 : ICON_LOW_OPACITY);
        textCostTotal.text = cardScriptableObject.foodRequirement.ToString();
        // TODO: Have amounts hack, move logic to hand
        GameObject hand = transform.parent.gameObject;
        FoodCard[] foodCards = hand.gameObject.GetComponentsInChildren<FoodCard>();
        int haveAmp = 0, haveRep = 0, haveSmM = 0, haveLgM = 0, haveTotal = 0;
        for (int i = 0; i < foodCards.Length; i++)
        {
            FoodCardScriptableObject.FoodType foodType = foodCards[i].GetType().foodType;
            switch (foodType)
            {
                case FoodCardScriptableObject.FoodType.AMPHIBIAN:
                    haveAmp += 1;
                    if (cardScriptableObject.foodAmphibian) haveTotal += 1;
                    break;
                case FoodCardScriptableObject.FoodType.REPTILE:
                    haveRep += 1;
                    if (cardScriptableObject.foodReptile) haveTotal += 1;
                    break;
                case FoodCardScriptableObject.FoodType.SMALL_MAMMAL:
                    haveSmM += 1;
                    if (cardScriptableObject.foodSmallMammal) haveTotal += 1;
                    break;
                case FoodCardScriptableObject.FoodType.LARGE_MAMMAL:
                    haveLgM += 1;
                    if (cardScriptableObject.foodLargeMammal) haveTotal += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        textHaveAmphibian.text = haveAmp.ToString();
        textHaveReptile.text = haveRep.ToString();
        textHaveSmallMammal.text = haveSmM.ToString();
        textHaveLargeMammal.text = haveLgM.ToString();
        textHaveTotal.text = haveTotal.ToString();
        textCostTotal.text = cardScriptableObject.foodRequirement.ToString();
        if (haveTotal >= cardScriptableObject.foodRequirement)
        {
            iconCheck.gameObject.SetActive(true);
            iconX.gameObject.SetActive(false);
        }
        else
        {
            iconX.gameObject.SetActive(true);
            iconCheck.gameObject.SetActive(false);
        }
        
        helperContainer.SetActive(true);
    }

    public void Close()
    {
        // Set overall opacity to zero
        // Reset all
        helperContainer.SetActive(false);
    }
}