using TMPro;
using UnityEngine;

public class FoodCard : MonoBehaviour
{
    [SerializeField] private FoodCardScriptableObject cardScriptableObject;
    [SerializeField] private TMP_Text textTitle;
    
    public void SetType(FoodCardScriptableObject type)
    {
        cardScriptableObject = type;
    }
    
    public void Initialize()
    {
        textTitle.text = cardScriptableObject.FoodTypeToString(cardScriptableObject.foodType);
    }
}
