using UnityEngine;

public class FoodPile : MonoBehaviour
{
    [SerializeField] private FoodCardScriptableObject.FoodType foodType;

    public void Draw()
    {
       FoodCardScriptableObject card = ScriptableObject.CreateInstance<FoodCardScriptableObject>();
       card.foodType = foodType;
       Events.EventDrawCard.Invoke(card);
    }
}
