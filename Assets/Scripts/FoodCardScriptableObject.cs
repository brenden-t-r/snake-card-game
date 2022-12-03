using UnityEditor;
using UnityEngine;

public class FoodCardScriptableObject : ScriptableObject, CardBase
{
    public Sprite sprite;
    public FoodType foodType;

    public enum FoodType
    {
        AMPHIBIAN=0,
        REPTILE=1,
        SMALL_MAMMAL=2,
        LARGE_MAMMAL=3
    }
    
    public CardBase.CardType GetCardType()
    {
        return CardBase.CardType.FOOD;
    }
    
    #if UNITY_EDITOR
    [MenuItem("Assets/Create/Food Card")]
    public static void CreateCard()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Card", "New Card", "Asset", "Save Card", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(CreateInstance<FoodCardScriptableObject>(), path);
    }
    #endif
}
