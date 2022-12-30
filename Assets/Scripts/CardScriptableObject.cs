using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardScriptableObject : ScriptableObject, CardBase
{
    public Material material;
    public Sprite sprite;
    public Sprite spriteInPlay;
    public string title;
    public int attack;
    public int health;
    public string funFact;
    public string description;
    public Ecosystem ecosystem;
    public List<Mechanic> mechanics;
    
    public enum Ecosystem
    {
        DESERT=0,
        FOREST=1,
        JUNGLE=2,
        WATER=3
    }
    
    public enum Mechanic
    {
        VENOMOUS=0,
        CANNIBAL=1
    }
    
    public CardBase.CardType GetCardType()
    {
        return CardBase.CardType.SNAKE;
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/Card")]
    public static void CreateCard()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Card", "New Card", "Asset", "Save Card", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(CreateInstance<CardScriptableObject>(), path);
    }
    #endif
}