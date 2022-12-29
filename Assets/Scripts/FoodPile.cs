using UnityEngine;

public class FoodPile : MonoBehaviour
{
    [SerializeField] private FoodCardScriptableObject cardScriptableObject;

    public void Draw()
    {
        Events.EventDrawCard.Invoke(cardScriptableObject);
    }
}
