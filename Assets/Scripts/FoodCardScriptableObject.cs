using System;
using System.Diagnostics;
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

    public string FoodTypeToString()
    {
        switch (foodType)
        {
            case FoodType.AMPHIBIAN:
                return "Amphibian";
            case FoodType.REPTILE:
                return "Reptile";
            case FoodType.SMALL_MAMMAL:
                return "Small Mammal";
            case FoodType.LARGE_MAMMAL:
                return "Large Mammal";
            default:
                throw new ArgumentOutOfRangeException();
        }
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
